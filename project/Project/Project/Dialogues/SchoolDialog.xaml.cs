using System.Windows;
using Models;

namespace Project.Dialogues;

public partial class SchoolDialog : Window {

	public School School { get; set; }
	public Address Address { get; set; }
	
	public SchoolDialog(School school = null, Address address = null) {
		School = school ?? new School();
		Address = address ?? new Address();
		InitializeComponent();
		DataContext = this;
	}
	
	private void Save(object sender, RoutedEventArgs e) {
		Close();
	}
}