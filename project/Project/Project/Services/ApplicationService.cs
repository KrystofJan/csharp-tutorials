using DatabaseHandler;
using Models;

namespace Project.Services;

public class ApplicationService {
	public static List<Application> FindAllAddresses() {
		return Database<Application>.SelectAll();
	}	
	public static int CreateNewAddress(Application s) {
		return Database<Application>.Insert(s);
	}

	public static void UpdateAddress(Application a) {
		Database<Application>.Update(a);
	}
	
	public static void DeleteAddress(Application a) {
		Database<Application>.Delete(a);
	}
}