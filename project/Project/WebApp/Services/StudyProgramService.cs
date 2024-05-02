using DatabaseHandler;
using DatabaseHandler.DatabaseUtility.WhereCondition;
using Models;
namespace WebApp.Services;

public class StudyProgramService {
	public List<StudyProgram> SelectedProgramsSet { get; set; } = new List<StudyProgram>();
	public int ProgramCount { get => SelectedProgramsSet.Count; }
	public static List<StudyProgram> GetStudyProgramByAnyAttribute(string value) {
		if (value == "%%") {
			return new List<StudyProgram>();
		}
		object val = value;
		List<School> schools = new List<School>();
		using (Condition schoolCondition = Condition.AddParam("SchoolName").Like(val).Build()) {
			 schools.AddRange(Database<School>.Select(schoolCondition));
		}
		
		List<StudyProgram> allStudyPrograms = Database<StudyProgram>.SelectAll();
		
		List<StudyProgram> conditionedStudyProgram = new List<StudyProgram>();
		using (Condition programCodeCondition = Condition.AddParam("StudyProgramCode").Like(val).Build()) {
			 conditionedStudyProgram.AddRange(Database<StudyProgram>.Select(programCodeCondition));
		} 
		
		List<StudyProgram> studyProgramsInSchool = allStudyPrograms
			.Where(s => schools.Any(school => s.School.SchoolId == school.SchoolId)).ToList();
		
		if (studyProgramsInSchool.Count > 0) {
			conditionedStudyProgram.AddRange(studyProgramsInSchool
				.Where(x => !studyProgramsInSchool
					.Select(x => x.StudyProgramId)
					.Intersect(conditionedStudyProgram.
						Select(x => x.StudyProgramId).ToList())
				.Contains(x.StudyProgramId)));
		}
		return conditionedStudyProgram;
	}

	public StudyProgram GetSpecificProgramById(int id) {
		StudyProgram sp = Database<StudyProgram>.SelectById(id);
		return sp;
	}

	public void Clear() {
		SelectedProgramsSet.Clear();	
	} 
	
	public void Add(StudyProgram s) {
		List<int> ids = SelectedProgramsSet.Select(x => x.StudyProgramId).ToList();
		if (!ids.Contains(s.StudyProgramId))
			SelectedProgramsSet.Add(s);	
	}

	public void Remove(int id) {
		StudyProgram? s = SelectedProgramsSet.FirstOrDefault(x => x.StudyProgramId == id);
		if(s != null)
			SelectedProgramsSet.Remove(s);
	}
}