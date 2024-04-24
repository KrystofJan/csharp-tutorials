using System.Windows;
using Models;

namespace Project.Dialogues;

public partial class StudentDialog : Window {

	public bool isSaved { get; set; }
	public Student Student { get; set; }
	
	public StudentDialog(Student s = null) {
		isSaved = false;
		InitializeComponent();
		Student = s ?? new Student();
		if (s == null) {
			Student.Address = new Address();
		}
		DataContext = this;
	}

	private void Save(object sender, RoutedEventArgs e) {
		isSaved = true;
		Close();
	}
}