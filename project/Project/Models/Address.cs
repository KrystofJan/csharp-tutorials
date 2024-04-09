using System.Security.Authentication;

namespace Models;

public class Address {
	public int AddressId { get; set; }
	public string Country { get; set; }
	public string Province { get; set; }
	public string City { get; set; }
	public string BuildingNumber { get; set; }
	public int  ApartamentNumber { get; set; }
	public string PostalCode { get; set; }
}