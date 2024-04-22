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
using Models;
using Project.Services;
using Project.Utils;

namespace Project;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {

	private StudentPage StudentPage { get; set; }
	private AddressPage AddressPage { get; set; }
	
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
		Main.Content = StudentPage;
	}

	private void SwitchStudent(object sender, RoutedEventArgs e) {
		Main.Content = new StudentPage();
	}

	private void SwitchAddress(object sender, RoutedEventArgs e) {
		Main.Content = new AddressPage();
	}
}
