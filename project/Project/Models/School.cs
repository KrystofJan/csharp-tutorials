namespace Models;

public class School {
	public int SchoolId { get; set; }
	public string SchoolName { get; set; }
	public string ContactPhone { get; set; }
	public string ContactEmail { get; set; }
	public Address Address { get; set; }
}