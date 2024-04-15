using DatabaseAttrs;

namespace Models;

public class Application {
	[Identifier]
	public int ApplicationId { get; set; }
	
	[ForeignObject]
	public StudyProgram PrimaryProgram { get; set; }
	
	[ForeignObject]
	public StudyProgram? SecondaryProgram { get; set; }
	
	[ForeignObject]
	public StudyProgram? TertiaryProgram { get; set; }
	
	[ForeignObject]
	public Student Student { get; set; }
	
	public DateTime SubmissionDate { get; set; }
}