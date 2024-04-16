using DatabaseAttrs;

namespace Models;

public class Application {
	[Identifier]
	public int ApplicationId { get; set; }
	
	[ForeignObject("PrimaryProgramId")]
	public StudyProgram PrimaryProgram { get; set; }
	
	[ForeignObject("SecondaryProgramId")]
	public StudyProgram? SecondaryProgram { get; set; }
	
	[ForeignObject("TertiaryProgramId")]
	public StudyProgram? TertiaryProgram { get; set; }
	
	[ForeignObject("StudentId")]
	public Student Student { get; set; }
	
	[InsertIgnore]
	public DateTime SubmissionDate { get; set; }
}