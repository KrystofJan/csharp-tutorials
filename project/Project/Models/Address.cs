using System.Security.Authentication;
using DatabaseAttrs;

namespace Models;

public class Address {
	[Identifier]
	public int AddressId { get; set; }
	public string Country { get; set; }
	public string Province { get; set; }
	public string Street { get; set; }
	public string City { get; set; }
	public string BuildingNumber { get; set; }
	public int  ApartamentNumber { get; set; }
	public string PostalCode { get; set; }
}