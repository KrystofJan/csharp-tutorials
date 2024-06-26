using System.Windows;
using System.Windows.Controls;
using Models;
using Project.Services;

namespace Project.Dialogues;

public partial class StudyProgramDialog : Window {
	public bool isSaved { get; set; }
	public StudyProgram StudyProgram { get; set; }
	public List<School> Schools { get; set; }
	
	public StudyProgramDialog(StudyProgram std = null) {
		InitializeComponent();
		isSaved = false;
		Schools = new List<School>();
		List<School> tmp = SchoolService.FindAllSchools();
		foreach (var school in tmp) {
			Schools.Add(school);
		}

		StudyProgram = std ?? new StudyProgram();

		if (std != null) {
			SchoolsCombobox.Text = std.School.ToString();
		}
		
		DataContext = this;
	}

	private void Save(object sender, RoutedEventArgs e) {
		isSaved = true;
		Close();
	}

	private void SelectSchool(object sender, RoutedEventArgs e) {
		StudyProgram.School = SchoolsCombobox.SelectedItem as School;
	}
}