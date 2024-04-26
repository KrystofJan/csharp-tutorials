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
}