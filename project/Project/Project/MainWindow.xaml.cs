using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Models;
using Project.Services;
using Project.Utils;

namespace Project;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {

	public BindingList<Student> Students { get; set; }
	public List<Student> ExportedStudents { get; set; }
	public string ButtonText { get; set; }
	
	public Thickness ButtonMargin { get; set; } = new Thickness(
		left: 10.0,
		top: 0,
		right: 10.0,
		bottom: 10
	);
	
	private object lockObj = new object();

	public MainWindow() {
		Students = new BindingList<Student>();
		ExportedStudents = new List<Student>();
		Thread t = new Thread(FetchData);
        t.Start();
		ButtonText = "Přidej studenta";
		InitializeComponent();
		DataContext = this;
	}

	private void FetchData() {
		Students.Clear();
		List<Student> std = StudentService.FindAllStudents();
		foreach (var student in std) {
			Students.Add(student);
		}
	}

	private void AddStudent(object sender, RoutedEventArgs e) {
		StudentDialog sd = new StudentDialog();
		sd.ShowDialog();
		int addressId = AddressService.CreateNewAddress(sd.Address);
		sd.Address.AddressId = addressId;
		sd.Student.Address = sd.Address;
		int studentId = StudentService.CreateNewStudent(sd.Student);
		sd.Student.StudentId = studentId;
		Students.Add(sd.Student);
	}

	private void Search(object sender, KeyEventArgs e) {
		if (textBox1.Text == "") {
			StudentGrid.ItemsSource = Students;
			return;
		}
		var filtered = Students.Where(s => 
			s.FirstName.ToLower().Contains(textBox1.Text.ToLower()) ||
			s.LastName.ToLower().Contains(textBox1.Text.ToLower())||
			s.Email.ToLower().Contains(textBox1.Text.ToLower()) ||
			s.Login.ToLower().Contains(textBox1.Text.ToLower()));

		StudentGrid.ItemsSource = filtered;            
	}

	private void Delete(object sender, RoutedEventArgs e) {
		Button btn = sender as Button;
		Student std = btn.DataContext as Student;
		StudentService.DeleteStudent(std);
		AddressService.DeleteAddress(std.Address);
		Students.Remove(std);
	}

	private void Update(object sender, RoutedEventArgs e) {
		Button btn = sender as Button;
		Student std = btn.DataContext as Student;
		StudentDialog sd = new StudentDialog(std, std.Address);
		sd.ShowDialog();
		StudentService.UpdateStudent(sd.Student);
		AddressService.UpdateAddress(sd.Address);
	}

	private void Export(object sender, RoutedEventArgs e) {
		ExportDialog exportSingle;
		if (ExportedStudents.Count == 0) {
			exportSingle = new ExportDialog(Students);
		}
		else if (ExportedStudents.Count > 1) {
			exportSingle = new ExportDialog(ExportedStudents);
		}
		else {
			exportSingle = new ExportDialog(ExportedStudents[0]);
		}
		exportSingle.ShowDialog();
	}

	private void AddExport(object sender, RoutedEventArgs e) {
		CheckBox btn = sender as CheckBox;
		Student std = btn.DataContext as Student;

		if (ExportedStudents.Contains(std)) {
			ExportedStudents.Remove(std);
		}
		else {
			ExportedStudents.Add(std);
		}
	}
}
