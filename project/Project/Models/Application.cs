using DatabaseAttrs;

namespace Models;

public class Application {
	[Identifier]
	public int AplicationId { get; set; }
	
	[ForeignObject]
	public StudyProgram PrimaryStudyProgram { get; set; }
	
	[ForeignObject]
	public StudyProgram? SecondaryStudyProgram { get; set; }
	
	[ForeignObject]
	public StudyProgram? TetiaryStudyProgram { get; set; }
	
	[ForeignObject]
	public Student Student { get; set; }
	
	public DateTime SubmissionDate { get; set; }
}