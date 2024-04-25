using DatabaseHandler;
using Models;
namespace WebApp.Services;

public class SchoolService {
	public SchoolService() {
	}

	public List<School> GetSchools() {
		return Database<School>.SelectAll();
	}
}