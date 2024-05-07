using System.ComponentModel;
using System.Net.Mime;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Models;
using Project.Dialogues;
using Project.Services;
using Application = Models.Application;

namespace Project.Pages;

public partial class ApplicationPage : Page {
	public BindingList<Application> Applications { get; set; }
	public List<Application> ExportedApplications { get; set; }
	public string ButtonText { get; set; }

	public Thickness ButtonMargin { get; set; } = new Thickness(
		left: 10.0,
		top: 0,
		right: 10.0,
		bottom: 10
	);

	public ApplicationPage() {
		Applications = new BindingList<Application>();
		ExportedApplications = new List<Application>();
		ButtonText = "Přidej adresu";
		Thread t = new Thread(FetchData);
		t.Start();
		// FetchData();
		InitializeComponent();
		DataContext = this;
	}

	private void FetchData() {
		Dispatcher.Invoke(() => {
			Applications.Clear();
			List<Application> std = ApplicationService.FindAllApplications();
			foreach (var student in std) {
				Applications.Add(student);
			}
		});
	}

	private void Search(object sender, KeyEventArgs e) {
		if (ApplicationSearch.Text == "") {
			ApplicationGrid.ItemsSource = Applications;
			return;
		}

		var filtered = Applications.Where(s =>
			s.Student.FirstName.ToLower().Contains(ApplicationSearch.Text.ToLower()) ||
			s.Student.LastName.ToLower().Contains(ApplicationSearch.Text.ToLower()));

		ApplicationGrid.ItemsSource = filtered;
	}

	private void Export(object sender, RoutedEventArgs e) {
		ExportDialog exportSingle;
		if (ExportedApplications.Count == 0) {
			exportSingle = new ExportDialog(Applications);
		}
		else if (ExportedApplications.Count > 1) {
			exportSingle = new ExportDialog(ExportedApplications);
		}
		else {
			exportSingle = new ExportDialog(ExportedApplications[0]);
		}

		exportSingle.ShowDialog();
	}

	private void AddApplication(object sender, RoutedEventArgs e) {
		ApplicationDialog sd = new ApplicationDialog();
		sd.ShowDialog();
		if (sd.isSaved) {
			int appId = ApplicationService.CreateNewApplication(sd.Application);
			sd.Application.ApplicationId = appId;
			Applications.Add(sd.Application);
		}
	}

	private void Delete(object sender, RoutedEventArgs e) {
		Button btn = sender as Button;
		Application std = btn.DataContext as Application;
		ApplicationService.DeleteApplication(std);
		Applications.Remove(std);
	}

	private void Update(object sender, RoutedEventArgs e) {
		Button btn = sender as Button;
		Application std = btn.DataContext as Application;
		ApplicationDialog sd = new ApplicationDialog(std);
		sd.ShowDialog();
		if (sd.isSaved) {
			ApplicationService.UpdateApplication(std);
		}
	}

	private void AddExport(object sender, RoutedEventArgs e) {
		CheckBox btn = sender as CheckBox;
		Application std = btn.DataContext as Application;

		if (ExportedApplications.Contains(std)) {
			ExportedApplications.Remove(std);
		}
		else {
			ExportedApplications.Add(std);
		}
	}
}