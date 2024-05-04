using DatabaseHandler;
using DatabaseHandler.DatabaseUtility.WhereCondition;
using Models;
namespace WebApp.Services;

public class StudyProgramService {

	public List<StudyProgram> GetStudyProgramBySchoolId(int value) {
		object val = value;
		List<StudyProgram> studyProgram = new List<StudyProgram>();
		using (Condition schoolCondition = Condition.AddParam("SchoolId").Like(val).Build()) {
			studyProgram.AddRange(Database<StudyProgram>.Select(schoolCondition));
		}
		return studyProgram;
	}



	public StudyProgram GetSpecificProgramById(int id) {
		StudyProgram sp = Database<StudyProgram>.SelectById(id);
		return sp;
	}

}