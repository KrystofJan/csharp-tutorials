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

		// Thread fetchThread = new Thread(FetchData);
		// fetchThread.Start();
		FetchData();
		ButtonText = "Přidej studenta";
		InitializeComponent();
		DataContext = this;
	}

	private void FetchData() {
		// while (true) {
			Students.Clear();
			List<Student> std = StudentService.FindAllStudents();
			foreach (var student in std) {
				Students.Add(student);
			}	
			// Thread.Sleep(100);
		// }
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

	private void TextBox1_OnKeyUp(object sender, KeyEventArgs e) {
		Console.WriteLine(textBox1.Text);
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
		// maybe lock here ????
		StudentDialog sd = new StudentDialog(std, std.Address);
		sd.ShowDialog();
		StudentService.UpdateStudent(sd.Student);
		AddressService.UpdateAddress(sd.Address);
	}
}