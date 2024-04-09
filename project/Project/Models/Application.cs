namespace Models;

public class Application {
	public int AplicationId { get; set; }
	public StudyProgram PrimaryStudyProgram { get; set; }
	public StudyProgram? SecondaryStudyProgram { get; set; }
	public StudyProgram? TetiaryStudyProgram { get; set; }
	public Student Student { get; set; }
	public DateTime SubmissionDate { get; set; }
}