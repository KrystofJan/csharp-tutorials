using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Models;
using Project.Dialogues;
using Project.Services;

namespace Project.Pages;

public partial class StudyProgramPage : Page {
	public BindingList<StudyProgram> StudyPrograms { get; set; }
	public List<StudyProgram> ExportedStudyPrograms { get; set; }

	public StudyProgramPage() {
		StudyPrograms = new BindingList<StudyProgram>();
		Thread t = new Thread(FetchData);
		t.Start();
		// FetchData();
		InitializeComponent();
		DataContext = this;
	}

	private void FetchData() {
		Dispatcher.Invoke(() => {
			StudyPrograms.Clear();
			List<StudyProgram> std = StudyProgramService.FindAllStudyPrograms();
			foreach (var program in std) {
				StudyPrograms.Add(program);
			}
		});
	}

	private void Search(object sender, KeyEventArgs e) {
		if (StudyProgramSearchBox.Text == "") {
			StudyProgramGrid.ItemsSource = StudyPrograms;
			return;
		}

		var filtered = StudyPrograms.Where(s =>
			s.School.SchoolName.ToLower().Contains(StudyProgramSearchBox.Text.ToLower()) ||
			s.StudyProgramCode.ToLower().Contains(StudyProgramSearchBox.Text.ToLower()) ||
			s.Summary.ToLower().Contains(StudyProgramSearchBox.Text.ToLower()));

		StudyProgramGrid.ItemsSource = filtered;
	}

	private void Delete(object sender, RoutedEventArgs e) {
		Button btn = sender as Button;
		StudyProgram std = btn.DataContext as StudyProgram;
		StudyProgramService.DeleteStudyProgram(std);
		StudyPrograms.Remove(std);
	}

	private void Update(object sender, RoutedEventArgs e) {
		Button btn = sender as Button;
		StudyProgram std = btn.DataContext as StudyProgram;
		StudyProgramDialog sd = new StudyProgramDialog(std);
		sd.ShowDialog();
		if (sd.isSaved)
			StudyProgramService.UpdateStudyProgram(sd.StudyProgram);
	}

	private void AddExport(object sender, RoutedEventArgs e) {
		CheckBox btn = sender as CheckBox;
		StudyProgram std = btn.DataContext as StudyProgram;

		if (ExportedStudyPrograms.Contains(std)) {
			ExportedStudyPrograms.Remove(std);
		}
		else {
			ExportedStudyPrograms.Add(std);
		}
	}

	private void AddStudyProgram(object sender, RoutedEventArgs e) {
		StudyProgramDialog sdp = new StudyProgramDialog();
		sdp.ShowDialog();
		if (sdp.isSaved) {
			int studyProgramId = StudyProgramService.CreateNewStudyProgram(sdp.StudyProgram);
			sdp.StudyProgram.StudyProgramId = studyProgramId;
			StudyPrograms.Add(sdp.StudyProgram);
		}
	}

	private void Export(object sender, RoutedEventArgs e) {
		ExportDialog exportSingle;
		if (ExportedStudyPrograms.Count == 0) {
			exportSingle = new ExportDialog(StudyPrograms);
		}
		else if (ExportedStudyPrograms.Count > 1) {
			exportSingle = new ExportDialog(ExportedStudyPrograms);
		}
		else {
			exportSingle = new ExportDialog(ExportedStudyPrograms[0]);
		}

		exportSingle.ShowDialog();
	}
}