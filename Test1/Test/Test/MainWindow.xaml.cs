using System.ComponentModel;
using System.Net.Http;
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
using System.Xml;
using System.Xml.Serialization;
using Test.Dialogue;
using Test.Models;

namespace Test;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {
	public BindingList<string> Mesta { get; set; }
	public BindingList<Subject> MestoList { get; set; }

	public MainWindow() {
		Mesta = new BindingList<string> {
			"Ostrava",
			"Brno",
			"Praha"
		};
		MestoList = new BindingList<Subject>();
		InitializeComponent();
		
		DataContext = this;
	}

	private async void ChooseCity(object sender, RoutedEventArgs e) {
		string text = CityBox.Text.ToLower();
		HttpClient client = new HttpClient();
		HttpResponseMessage response = await client.GetAsync($"https://dataor.justice.cz/api/file/zp-full-{text}-2024.xml");
		if (!response.IsSuccessStatusCode) {
			Console.WriteLine("Nejdu");
		}

		MestoList.Clear();
		string xmlData = await response.Content.ReadAsStringAsync();
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(xmlData);
		for (int i = 0; i < xmlDocument.ChildNodes[1].ChildNodes.Count;++i) {
			string nazev = xmlDocument.ChildNodes[1].ChildNodes[i].ChildNodes[0].InnerText;
			string ico = xmlDocument.ChildNodes[1].ChildNodes[i].ChildNodes[1].InnerText;
			string datum = xmlDocument.ChildNodes[1].ChildNodes[i].ChildNodes[2].InnerText;
			
			MestoList.Add(new Subject{nazev = nazev, ico = ico, zapisDatum = datum});	
		}
	}

	private void EditZaznam(object sender, RoutedEventArgs e) {
		Button context = (Button) sender;
		Subject s = (Subject) context.DataContext;
		EditDialogue editDialogue = new EditDialogue(s);
		
		editDialogue.ShowDialog();
		if (editDialogue.IsSaved) {
			s = editDialogue.Subject;
		}
	}
}