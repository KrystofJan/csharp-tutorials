using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Models;
using Project.Services;
using Project.Dialogues;

namespace Project;

public partial class SchoolPage : Page {

	public BindingList<School> Schools { get; set; }
	public List<School> ExportedSchools { get; set; }
	public string ButtonText { get; set; }
	
	public Thickness ButtonMargin { get; set; } = new Thickness(
		left: 10.0,
		top: 0,
		right: 10.0,
		bottom: 10
	);
	
	public SchoolPage() {
		Schools = new BindingList<School>();
		ExportedSchools = new List<School>();
		ButtonText = "Přidej školu";
		Thread t = new Thread(FetchData);
		t.Start();
		InitializeComponent();
		DataContext = this;
	}

	private void FetchData() {
		Schools.Clear();
		List<School> std = SchoolService.FindAllSchools();
		foreach (var school in std) {
			Schools.Add(school);
		}
	}

	private void AddAddress(object sender, RoutedEventArgs e) {
		SchoolDialog sd = new SchoolDialog();
		sd.ShowDialog();
		int addressId = AddressService.CreateNewAddress(sd.Address);
		sd.Address.AddressId = addressId;
		sd.School.Address = sd.Address;
		int schoolId = SchoolService.CreateNewSchool(sd.School);
		sd.School.SchoolId = schoolId;
		Schools.Add(sd.School);
	}

	private void Search(object sender, KeyEventArgs e) {
		if (SchoolSearchBox.Text == "") {
			SchoolGrid.ItemsSource = Schools;
			return;
		}
		var filtered = Schools.Where(s => 
			s.Address.City.ToLower().Contains(SchoolSearchBox.Text.ToLower()) ||
			s.Address.Street.ToLower().Contains(SchoolSearchBox.Text.ToLower())||
			s.Address.Country.ToLower().Contains(SchoolSearchBox.Text.ToLower()) ||
			s.Address.Country.ToLower().Contains(SchoolSearchBox.Text.ToLower()) ||
			s.SchoolName.ToLower().Contains(SchoolSearchBox.Text.ToLower()));

		SchoolGrid.ItemsSource = filtered;            
	}

	private void Delete(object sender, RoutedEventArgs e) {
		Button btn = sender as Button;
		School std = btn.DataContext as School;
		SchoolService.DeleteSchool(std);
		AddressService.DeleteAddress(std.Address);
		Schools.Remove(std);
	}

	private void Update(object sender, RoutedEventArgs e) {
		Button btn = sender as Button;
		School std = btn.DataContext as School;
		SchoolDialog sd = new SchoolDialog(std, std.Address);
		sd.ShowDialog();
		AddressService.UpdateAddress(std.Address);
		SchoolService.UpdateSchool(std);
	}

	private void Export(object sender, RoutedEventArgs e) {
		ExportDialog exportSingle;
		if (ExportedSchools.Count == 0) {
			exportSingle = new ExportDialog(Schools);
		}
		else if (ExportedSchools.Count > 1) {
			exportSingle = new ExportDialog(ExportedSchools);
		}
		else {
			exportSingle = new ExportDialog(ExportedSchools[0]);
		}
		exportSingle.ShowDialog();
	}

	private void AddExport(object sender, RoutedEventArgs e) {
		CheckBox btn = sender as CheckBox;
		School std = btn.DataContext as School;

		if (ExportedSchools.Contains(std)) {
			ExportedSchools.Remove(std);
		}
		else {
			ExportedSchools.Add(std);
		}
	}
}