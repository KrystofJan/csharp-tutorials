using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Models;
using Project.Services;
using Project.Dialogues;

namespace Project;

public partial class AddressPage : Page {
	public BindingList<Address> Addresses { get; set; }
	public List<Address> ExportedAddresses { get; set; }
	public string ButtonText { get; set; }

	public Thickness ButtonMargin { get; set; } = new Thickness(
		left: 10.0,
		top: 0,
		right: 10.0,
		bottom: 10
	);

	public AddressPage() {
		Addresses = new BindingList<Address>();
		ExportedAddresses = new List<Address>();
		ButtonText = "Přidej adresu";
		// TODO: Pridat vice vlaken do wpf -> JJ rikal, vyresit dispatcherem
		Thread t = new Thread(FetchData);
		t.Start();
		InitializeComponent();
		DataContext = this;
	}

	private void FetchData() {
		Dispatcher.Invoke(() => {
			Addresses.Clear();
			List<Address> std = AddressService.FindAllAddresses();
			foreach (var student in std) {
				Addresses.Add(student);
			}
		});
	}

	private void AddAddress(object sender, RoutedEventArgs e) {
		AddressDialogue sd = new AddressDialogue();
		sd.ShowDialog();
		if (sd.isSaved) {
			int addressId = AddressService.CreateNewAddress(sd.Address);
			sd.Address.AddressId = addressId;
			Addresses.Add(sd.Address);
		}
	}

	private void Search(object sender, KeyEventArgs e) {
		if (textBox1.Text == "") {
			AddressGrid.ItemsSource = Addresses;
			return;
		}

		var filtered = Addresses.Where(s =>
			s.City.ToLower().Contains(textBox1.Text.ToLower()) ||
			s.Street.ToLower().Contains(textBox1.Text.ToLower()) ||
			s.Country.ToLower().Contains(textBox1.Text.ToLower()) ||
			s.Province.ToLower().Contains(textBox1.Text.ToLower()));

		AddressGrid.ItemsSource = filtered;
	}

	private void Delete(object sender, RoutedEventArgs e) {
		Button btn = sender as Button;
		Address std = btn.DataContext as Address;
		AddressService.DeleteAddress(std);
		Addresses.Remove(std);
	}

	private void Update(object sender, RoutedEventArgs e) {
		Button btn = sender as Button;
		Address std = btn.DataContext as Address;
		AddressDialogue sd = new AddressDialogue(std);
		sd.ShowDialog();
		if (sd.isSaved) {
			AddressService.UpdateAddress(std);
		}
	}

	private void Export(object sender, RoutedEventArgs e) {
		ExportDialog exportSingle;
		if (ExportedAddresses.Count == 0) {
			exportSingle = new ExportDialog(Addresses);
		}
		else if (ExportedAddresses.Count > 1) {
			exportSingle = new ExportDialog(ExportedAddresses);
		}
		else {
			exportSingle = new ExportDialog(ExportedAddresses[0]);
		}

		exportSingle.ShowDialog();
	}

	private void AddExport(object sender, RoutedEventArgs e) {
		CheckBox btn = sender as CheckBox;
		Address std = btn.DataContext as Address;

		if (ExportedAddresses.Contains(std)) {
			ExportedAddresses.Remove(std);
		}
		else {
			ExportedAddresses.Add(std);
		}
	}
}