using DatabaseAttrs;

namespace Models;

public class StudyProgram {
	[Identifier]
	public int StudyProgramId { get; set; }
	public string StudyProgramCode { get; set; }

	[ForeignObject]
	public School School { get; set; }
	public string Summary { get; set; }
	public int MaximumCapacity { get; set; }
	public int CurrentCapacity { get; set; }
}