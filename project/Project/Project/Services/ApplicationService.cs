using DatabaseHandler;
using Models;

namespace Project.Services;

public class ApplicationService {
	public static List<Application> FindAllApplications() {
		return Database<Application>.SelectAll();
	}	
	public static int CreateNewApplication(Application s) {
		return Database<Application>.Insert(s);
	}

	public static void UpdateApplication(Application a) {
		Database<Application>.Update(a);
	}
	
	public static void DeleteApplication(Application a) {
		Database<Application>.Delete(a);
	}
}