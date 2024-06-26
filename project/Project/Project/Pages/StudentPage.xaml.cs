using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Models;
using Project.Services;
using Project.Dialogues;

namespace Project;

public partial class StudentPage : Page {
	public BindingList<Student> Students { get; set; }
	public List<Student> ExportedStudents { get; set; }
	public string ButtonText { get; set; }

	public Thickness ButtonMargin { get; set; } = new Thickness(
		left: 10.0,
		top: 0,
		right: 10.0,
		bottom: 10
	);

	public StudentPage() {
		Students = new BindingList<Student>();
		ExportedStudents = new List<Student>();
		Thread t = new Thread(FetchData);
		t.Start();
		ButtonText = "Přidej studenta";
		InitializeComponent();
		DataContext = this;
	}

	private void FetchData() {
//		while (true) {
			Dispatcher.Invoke(() => {
				Students.Clear();
				List<Student> std = StudentService.FindAllStudents();
				foreach (var student in std) {
					Students.Add(student);
				}
			});
//			Thread.Sleep(1000);
//		}
	}

	private void AddStudent(object sender, RoutedEventArgs e) {
		StudentDialog sd = new StudentDialog();
		sd.ShowDialog();
		if (sd.isSaved) {
			int studentId = StudentService.CreateNewStudent(sd.Student);
			sd.Student.StudentId = studentId;
			Students.Add(sd.Student);
		}
	}

	private void Search(object sender, KeyEventArgs e) {
		if (StudentSearchBox.Text == "") {
			StudentGrid.ItemsSource = Students;
			return;
		}

		var filtered = Students.Where(s =>
			s.FirstName.ToLower().Contains(StudentSearchBox.Text.ToLower()) ||
			s.LastName.ToLower().Contains(StudentSearchBox.Text.ToLower()) ||
			s.Email.ToLower().Contains(StudentSearchBox.Text.ToLower()) ||
			s.Login.ToLower().Contains(StudentSearchBox.Text.ToLower()));

		StudentGrid.ItemsSource = filtered;
	}

	private void Delete(object sender, RoutedEventArgs e) {
		Button btn = sender as Button;
		Student std = btn.DataContext as Student;
		StudentService.DeleteStudent(std);
		Students.Remove(std);
	}

	private void Update(object sender, RoutedEventArgs e) {
		Button btn = sender as Button;
		Student std = btn.DataContext as Student;
		StudentDialog sd = new StudentDialog(std);
		sd.ShowDialog();
		if (sd.isSaved)
			StudentService.UpdateStudent(sd.Student);
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