using System.Windows;
using System.Windows.Controls;
using Models;
using Project.Services;

namespace Project.Dialogues;

public partial class StudyProgramDialog : Window {
	public StudyProgram StudyProgram { get; set; }
	public List<School> Schools { get; set; }
	public School SelectedSchool { get; set; }
	
	public StudyProgramDialog(StudyProgram std = null) {
		Schools = new List<School>();
		List<School> tmp = SchoolService.FindAllSchools();
		foreach (var school in tmp) {
			Schools.Add(school);
		}

		StudyProgram = std ?? new StudyProgram();
		
		DataContext = this;
		InitializeComponent();
	}

	private void Save(object sender, RoutedEventArgs e) {
		Close();
	}

	private void SelectSchool(object sender, RoutedEventArgs e) {
		StudyProgram.School = SchoolsCombobox.SelectedItem as School;
		// SchoolsCombobox.Text = StudyProgram.School.SchoolName;
	}
}