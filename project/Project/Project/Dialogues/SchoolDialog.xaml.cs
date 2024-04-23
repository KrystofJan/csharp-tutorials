using System.Windows;
using Models;

namespace Project.Dialogues;

public partial class SchoolDialog : Window {

	public School School { get; set; }
	
	public SchoolDialog(School school = null) {
		School = school ?? new School();
		if (School.Address == null) {
			School.Address = new Address();
		}
		InitializeComponent();
		DataContext = this;
	}
	
	private void Save(object sender, RoutedEventArgs e) {
		Close();
	}
}