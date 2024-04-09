using System.Windows;

namespace CSharp6_WPF;

public partial class CustomerForm : Window {
	public Customer Customer { get; set; }

	public CustomerForm(Customer c = null) {
		InitializeComponent();
		Customer = c ?? new Customer();
		DataContext = Customer;
	}

	private void Save(object sender, RoutedEventArgs e) {
		this.Close();
	}
}