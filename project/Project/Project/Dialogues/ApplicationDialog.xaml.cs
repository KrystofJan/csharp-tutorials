using System.Windows;
using System.Windows.Controls;
using Models;
using Project.Services;
using Application = Models.Application;
namespace Project.Dialogues;

public partial class ApplicationDialog : Window {
	public bool isSaved { get; set; }
	public Application Application { get; set; }
	public List<StudyProgram> StudyPrograms { get; set; }
	public List<Student> Students { get; set; }
	public StudyProgram PrimProgram { get; set; }
	public ApplicationDialog(Application a = null) {
		InitializeComponent();
		
		isSaved = false;
		Application = a ?? new Application();
		StudyPrograms = new List<StudyProgram>();
		List<StudyProgram> tmpStudyPrograms = StudyProgramService.FindAllStudyPrograms();
		foreach (var std in tmpStudyPrograms) {
			StudyPrograms.Add(std);
		}		
		Students = new List<Student>();
		List<Student> tmpStudents = StudentService.FindAllStudents();
		foreach (var std in tmpStudents) {
			Students.Add(std);
		}

		if (a != null) {
			PrimarySchoolCombobox.Text = a.PrimaryProgram.ToString();
			SecondarySchoolCombobox.Text = a.SecondaryProgram.ToString();
			TertiarySchoolCombobox.Text = a.TertiaryProgram.ToString();
			StudentCombobox.Text = a.Student.ToString();
		}
		DataContext = this;
	}

	private void Save(object sender, RoutedEventArgs e) {
		isSaved = true;
		Application.SubmissionDate = DateTime.Now;
		Close();
	}

	private void SelectPrimary(object sender, SelectionChangedEventArgs e) {
		Application.PrimaryProgram = PrimarySchoolCombobox.SelectedItem as StudyProgram;
	}

	private void SelectSecondary(object sender, SelectionChangedEventArgs e) {
		Application.SecondaryProgram = SecondarySchoolCombobox.SelectedItem as StudyProgram;
	}

	private void SelectTertiary(object sender, SelectionChangedEventArgs e) {
		Application.TertiaryProgram = TertiarySchoolCombobox.SelectedItem as StudyProgram;
	}

	private void SelectStudent(object sender, SelectionChangedEventArgs e) {
		Application.Student = StudentCombobox.SelectedItem as Student;
	}
}