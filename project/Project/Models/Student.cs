using DatabaseAttrs;

namespace Models;

public class Student {
	[Identifier]
	public int StudentId { get; set; }
	public string Login { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	
	[ForeignObject]
	public Address Address { get; set; }
	public string Phone { get; set; }
	public string Email { get; set; }
}