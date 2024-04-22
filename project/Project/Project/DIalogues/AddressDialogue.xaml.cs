using System.Windows;
using Models;

namespace Project;

public partial class AddressDialogue : Window {
	public Address Address { get; set; }
	public AddressDialogue(Address a = null) {
		InitializeComponent();
		Address = a ?? new Address();
		DataContext = this;
	}
	
	private void Save(object sender, RoutedEventArgs e) {
		Close();
	}
}