// See https://aka.ms/new-console-template for more information

using DatabaseHandler;
using Models;

List<Student> students = Database<Student>.SelectAll();
List<Address> Addresses = Database<Address>.SelectAll();
List<Application> Apps = Database<Application>.SelectAll();
List<StudyProgram> StudyPrograms = Database<StudyProgram>.SelectAll();
List<School> schools = Database<School>.SelectAll();

Console.WriteLine("Hello world! ");

Address a = new Address() {
	ApartamentNumber = 2,
	BuildingNumber = "23",
	City = "FM",
	Country = "CZ",
	PostalCode = "738 01",
	Province = "Moravskoslezsky Kraj",
	Street = "Sporilov"
};
a.AddressId = Database<Address>.Insert(a);
a.City = "OVA";

Database<Address>.Update(a);
Database<Address>.Delete(a);

Student s = Database<Student>.SelectById(1);

Console.WriteLine("Hello world! ");
