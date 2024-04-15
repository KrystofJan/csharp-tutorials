using System;
using Models;

namespace DatabaseHandler;
// Note: actual namespace depends on the project name.

internal class Program {
	
	static void Main(string[] args) {
		object addr = Database<Application>.Select(1);
		List<object> studyPrograms = Database<StudyProgram>.SelectAll();

		Address a = new Address();
		a.AddressId = 2;
		a.City = "Coppenhagen";
		a.Country = "Germany";
		a.Province = "Bavorsko";
		a.PostalCode = "73801";
		a.BuildingNumber = "155";
		a.ApartamentNumber = 7;
		a.Street = "Dr. Malého";

		Student student = new Student();
		student.Address = a;
		student.Email = "jan.zahradnik@profiq.com";
		student.Login = "ZAH0089";
		student.Phone = "+888 333 444 555";
		student.FirstName = "Petr";
		student.LastName = "Pokorny";

		bool worked = Database<Student>.Insert(student);
		
		
		Console.WriteLine();
	}
}