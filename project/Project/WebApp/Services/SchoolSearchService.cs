using DatabaseHandler;
using DatabaseHandler.DatabaseUtility.WhereCondition;
using Models;
namespace WebApp.Services;

public class SchoolSearchService {

	public List<School> SearchedSchools { get; set; } = new List<School>();
	public SchoolSearchService() {
		SearchedSchools = Database<School>.SelectAll();
	}

	public List<School> GetSchoolsByName(string value) {
		SearchedSchools.Clear();
		if (value == "%%") {
			return Database<School>.SelectAll();
		}
		object val = value;
		List<School> schools = new List<School>();
		using (Condition schoolCondition = Condition.AddParam("SchoolName").Like(val).Build()) {
			schools.AddRange(Database<School>.Select(schoolCondition));
		}
		SearchedSchools.AddRange(schools);
		return schools;
	}

	public void Clear() {
		SearchedSchools.Clear();
	}

	public void Add(List<School> s) {
		List<int> ids = SearchedSchools.Select(x => x.SchoolId).ToList();
		foreach (School school in s) {
			if (!ids.Contains(school.SchoolId))
				SearchedSchools.Add(school);
		}
	}
	public void Add(School s) {
		List<int> ids = SearchedSchools.Select(x => x.SchoolId).ToList();
		if (!ids.Contains(s.SchoolId))
			SearchedSchools.Add(s);
	}

	public void Remove(int id) {
		School? s = SearchedSchools.FirstOrDefault(x => x.SchoolId == id);
		if (s != null)
			SearchedSchools.Remove(s);
	}

}