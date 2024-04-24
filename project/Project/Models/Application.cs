using System.ComponentModel;
using System.Runtime.CompilerServices;
using DatabaseAttrs;

namespace Models;

public class Application{
	[Identifier]
	public int ApplicationId { get; set; }
	
	[ForeignObject("PrimaryProgramId")]
	public StudyProgram PrimaryProgram { get; set; }

	[ForeignObject("SecondaryProgramId")]
	public StudyProgram? SecondaryProgram { get; set; }	
	// public int? SecondaryProgramId { get; set; }

	[ForeignObject("TertiaryProgramId")]
	public StudyProgram? TertiaryProgram { get; set; }
	// public int? TertiaryProgramId { get; set; }

	// public int StudentId { get; set; }
	[ForeignObject("StudentId")]
	public Student Student { get; set; }
	
	[InsertIgnore]
	public DateTime SubmissionDate { get; set; }

	public event PropertyChangedEventHandler? PropertyChanged;

	protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null) {
		if (EqualityComparer<T>.Default.Equals(field, value)) return false;
		field = value;
		OnPropertyChanged(propertyName);
		return true;
	}
}