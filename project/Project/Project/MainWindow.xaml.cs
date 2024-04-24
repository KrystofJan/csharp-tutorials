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
using Project.Pages;

namespace Project;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {

	private StudentPage StudentPage { get; set; }
	private AddressPage AddressPage { get; set; }
	private SchoolPage SchoolPage { get; set; }
	public StudyProgramPage StudyProgramPage { get; set; }
	public ApplicationPage ApplicationPage { get; set; }
	
	public Thickness ButtonMargin { get; set; } = new Thickness(
		left: 16.0,
		top: 8,
		right: 16.0,
		bottom: 8
	);
	public MainWindow() {
		InitializeComponent();
		StudentPage = new StudentPage();
		AddressPage = new AddressPage();
		SchoolPage = new SchoolPage();
		StudyProgramPage = new StudyProgramPage();
		ApplicationPage = new ApplicationPage();
		Main.Content = StudentPage;
	}

	private void SwitchStudent(object sender, RoutedEventArgs e) {
		Main.Content = StudentPage;
	}

	private void SwitchAddress(object sender, RoutedEventArgs e) {
		Main.Content = AddressPage;
	}

	private void SwitchSchool(object sender, RoutedEventArgs e) {
		Main.Content = SchoolPage;
	}

	private void SwitchStudyProgram(object sender, RoutedEventArgs e) {
		Main.Content = StudyProgramPage;
	}

	private void SwitchApplication(object sender, RoutedEventArgs e) {
		Main.Content = ApplicationPage;
	}
}
