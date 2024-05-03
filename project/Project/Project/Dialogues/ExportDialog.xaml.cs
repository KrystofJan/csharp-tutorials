using System.IO;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Windows;
using System.Xml.Serialization;
using Microsoft.Win32;

namespace Project.Dialogues;

enum TypeOfExport {
	EXPORT_SINGLE,
	EXPORT_LIST
}

public partial class ExportDialog : Window {
	public object SerializableObj { get; set; }
	public List<object> SerializableObjList { get; set; }
	private TypeOfExport ExpType { get; set; }
	
	public ExportDialog(object ser) {
		SerializableObj = ser;
		ExpType = TypeOfExport.EXPORT_SINGLE;
		InitializeComponent();
	}
	
	public ExportDialog(List<object> ser) {
		SerializableObjList = ser;
		ExpType = TypeOfExport.EXPORT_LIST;
		InitializeComponent();
	}

	private void JsonOp(object sender, RoutedEventArgs e) {
		SaveFileDialog dlg = new SaveFileDialog();
		dlg.Filter = "JSON Files (*.json)|*.json";
		dlg.DefaultExt = "json";
		dlg.AddExtension = true;
		bool? result = dlg.ShowDialog();
		if (result != true) {
			return;
		}
		string filename = dlg.FileName;
		Task.Run(() => ExportJson(filename));
		Close();
	}
	
	private async Task ExportJson(string fileName) {
		try {
			string json;
			if (ExpType == TypeOfExport.EXPORT_LIST) {
				json = JsonSerializer.Serialize(SerializableObjList, new JsonSerializerOptions{WriteIndented = true});
				await File.WriteAllTextAsync(fileName, json);
				return;
			}
			json = JsonSerializer.Serialize(SerializableObj, new JsonSerializerOptions{WriteIndented = true});
			await File.WriteAllTextAsync(fileName, json);
		}
		catch (Exception e) {
			throw e;
		}
	}
	private void XmlOp(object sender, RoutedEventArgs e) {
		SaveFileDialog dlg = new SaveFileDialog();
		dlg.Filter = "XML Files (*.xml)|*.xml";
		dlg.DefaultExt = "xml";
		dlg.AddExtension = true;
		bool? result = dlg.ShowDialog();
		if (result != true) {
			return;
		}
		string filename = dlg.FileName;
		Task.Run(() => ExportXml(filename));
		Close();
	}
	private async Task ExportXml(string fileName) {
		try {
			if (ExpType == TypeOfExport.EXPORT_LIST) {
				XmlSerializer ser = new XmlSerializer(SerializableObjList.GetType());
				using FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate);
				await Task.Run(() => ser.Serialize(fs, SerializableObjList));
				fs.Close();
				return;
			}
			XmlSerializer serializer = new XmlSerializer(SerializableObj.GetType());
			using FileStream fst = new FileStream(fileName, FileMode.OpenOrCreate);
			await Task.Run(() => serializer.Serialize(fst, SerializableObj));
			fst.Close();
		}
		catch (Exception e) {
			throw e;
		}
	}
}