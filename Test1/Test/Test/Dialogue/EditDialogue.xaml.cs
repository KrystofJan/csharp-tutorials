using System.Windows;
using Test.Models;

namespace Test.Dialogue;

public partial class EditDialogue : Window {
	public bool IsSaved { get; set; }
	public Subject Subject { get; set; }
	public EditDialogue(Subject? subject = null) {
		Subject = subject ?? new Subject();
		IsSaved = false;
		DataContext = Subject;
		InitializeComponent();
	}

	private void Save(object sender, RoutedEventArgs e) {
		IsSaved = true;
		Close();
	}
}