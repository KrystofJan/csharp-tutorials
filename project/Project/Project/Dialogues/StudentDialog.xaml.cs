using System.Windows;
using Models;

namespace Project.Dialogues;

public partial class StudentDialog : Window {

	public Student Student { get; set; }
	public Address Address { get; set; }
	
	public StudentDialog(Student s = null, Address a = null) {
		InitializeComponent();
		Student = s ?? new Student();
		Address = a ?? new Address();
		DataContext = this;
	}

	private void Save(object sender, RoutedEventArgs e) {
		Close();
	}
}