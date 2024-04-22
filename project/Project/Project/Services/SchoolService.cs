using DatabaseHandler;
using Models;

namespace Project.Services;

public class SchoolService {
	public static List<School> FindAllSchools() {
		return Database<School>.SelectAll();
	}	
	public static int CreateNewSchool(School s) {
		return Database<School>.Insert(s);
	}

	public static void UpdateSchool(School a) {
		Database<School>.Update(a);
	}
	
	public static void DeleteSchool(School a) {
		Database<School>.Delete(a);
	}
}