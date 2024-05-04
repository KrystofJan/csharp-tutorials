using DatabaseHandler;
using DatabaseHandler.DatabaseUtility.WhereCondition;
using Models;
namespace WebApp.Services;

public class SchoolService {
	public SchoolService() {
	}

	public List<School> GetSchools() {
		return Database<School>.SelectAll();
	}

	public School GetSchoolById(int id) {
		return Database<School>.SelectById(id);
	}

	public List<School> GetSchoolsByName(string value) {
		object val = value;
		List<School> schools = new List<School>();
		using (Condition schoolCondition = Condition.AddParam("SchoolName").Like(val).Build()) {
			schools.AddRange(Database<School>.Select(schoolCondition));
		}
		return schools;
	}
}