using DatabaseHandler;
using Models;

namespace Project.Services;

public class AddressService {
	public static List<Address> FindAllAddresses() {
		return Database<Address>.SelectAll();
	}	
	public static int CreateNewAddress(Address s) {
		return Database<Address>.Insert(s);
	}

	public static void UpdateAddress(Address a) {
		Database<Address>.Update(a);
	}
	
	public static void DeleteAddress(Address a) {
		Database<Address>.Delete(a);
	}
}