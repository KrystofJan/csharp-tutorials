using System.Collections.ObjectModel;
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
using Microsoft.VisualBasic;

namespace CSharp6_WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {

	// BindingList, nebo cokoliv, dokud to implementuje INotifyCollectionChanged
	public ObservableCollection<Customer> customers { get; set; } = new ObservableCollection<Customer> {
		new Customer{FirstName = "Jan", LastName = "Janousek", Id = 1, Age = 30},
		new Customer{FirstName = "Jean", LastName = "Jeanousek", Id = 2, Age = 31}
	};

	public Thickness ButtonMargin { get; set; } = new Thickness(
		left: 10.0,
		top: 0,
		right: 10.0,
		bottom: 10
	);
	
	public string ButtonText { get; set; } = "Přidat zákazníka";

	public MainWindow() {
		InitializeComponent();
		this.DataContext = this;
		// gridEl.ItemsSource = Customers;
	}

	private void AddCustomer(object sender, RoutedEventArgs e) {
		CustomerForm cf = new CustomerForm();
		cf.ShowDialog();
		
		customers.Add(cf.Customer);
		
		// customers.Add(new Customer {
		// 	FirstName = "Dan",
		// 	LastName = "Danousek",
		// 	Age=16,
		// 	Id = 3
		// });
	}

	private void RemoveCustomer(object sender, RoutedEventArgs e) {
		Button btn = sender as Button;
		customers.Remove(btn.DataContext as Customer);
	}

	private void Anonynize(object sender, RoutedEventArgs e) {
		Button btn = sender as Button;
		Customer myCustomer = btn.DataContext as Customer;
		myCustomer.FirstName = "********";
		myCustomer.LastName = "********";
	}

	private void EditCustomer(object sender, RoutedEventArgs e) {
		Button btn = sender as Button;
		Customer myCustomer = btn.DataContext as Customer;
		CustomerForm cf = new CustomerForm(myCustomer);
		cf.ShowDialog();
	}
}