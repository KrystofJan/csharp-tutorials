using System.ComponentModel;
using System.Runtime.CompilerServices;
using DatabaseAttrs;

namespace Models;

public class StudyProgram {
	[Identifier]
	public int StudyProgramId { get; set; }
	public string StudyProgramCode { get; set; }

	[ForeignObject("SchoolId")]
	public School School { get; set; }
	public string Summary { get; set; }
	public int MaximumCapacity { get; set; }
	public int CurrentCapacity { get; set; }
	public override string ToString() {
		return $"{StudyProgramCode} - {Summary}" ;
	}

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