using DatabaseHandler;
using DatabaseHandler.DatabaseUtility.WhereCondition;
using Models;
namespace WebApp.Services;

public class StudyProgramService {
	public static List<StudyProgram> GetStudyProgramByAnyAttribute(string value) {
		object val = value;
		Condition schoolCondition = Condition.AddParam("SchoolName").Like(val).Build();
		List<School> schools = Database<School>.Select(schoolCondition);
		List<StudyProgram> allStudyPrograms = Database<StudyProgram>.SelectAll();
		Condition programCodeCondition = Condition.AddParam("StudyProgramCode").Like(val).Build();
		List<StudyProgram> conditionedStudyProgram = Database<StudyProgram>.Select(programCodeCondition);
		List<StudyProgram> studyProgramsInSchool = allStudyPrograms
			.Where(s => schools.Any(school => s.School.SchoolId == school.SchoolId)).ToList();

		if (studyProgramsInSchool.Count > 0) {
			conditionedStudyProgram.AddRange(studyProgramsInSchool);
		}

		return conditionedStudyProgram;
	}
}