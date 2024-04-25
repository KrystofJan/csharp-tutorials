using DatabaseHandler;
using Models;
namespace WebApp.Services;

public class SchoolService {
	public SchoolService() {
	}

	public List<Address> List() {
		return Database<Address>.SelectAll();
	}
}