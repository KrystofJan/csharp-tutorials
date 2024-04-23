using DatabaseHandler;
using Models;

namespace Project.Services;

public class StudyProgramService {
	public static List<StudyProgram> FindAllStudyPrograms() {
		return Database<StudyProgram>.SelectAll();
	}	
	
	public static int CreateNewStudyProgram(StudyProgram s) {
		return Database<StudyProgram>.Insert(s);
	}

	public static void DeleteStudyProgram(StudyProgram s) {
		Database<StudyProgram>.Delete(s);
	}

	public static void UpdateStudyProgram(StudyProgram s) {
		Database<StudyProgram>.Update(s);
	}
}