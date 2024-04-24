using System.Windows;
using Models;

namespace Project.Dialogues;

public partial class AddressDialogue : Window { 
	
	public bool isSaved { get; set; }
	public Address Address { get; set; }
	public AddressDialogue(Address a = null) {
		isSaved = false;
		InitializeComponent();
		Address = a ?? new Address();
		DataContext = this;
	}
	
	private void Save(object sender, RoutedEventArgs e) {
		isSaved = true;
		Close();
	}
}