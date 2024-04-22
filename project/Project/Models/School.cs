using DatabaseAttrs;

namespace Models;

public class School {
	[Identifier]
	public int SchoolId { get; set; }
	public string SchoolName { get; set; }
	public string ContactPhone { get; set; }
	public string ContactEmail { get; set; }
	
	[ForeignObject("AddressId")]
	public Address Address { get; set; }
}