using System;
using Models;

namespace DatabaseHandler;
// Note: actual namespace depends on the project name.

internal class Program {
	
	static void Main(string[] args) {
		object addr = Database<Application>.Select(1);
		object studyPrograms = Database<Application>.Select(1);

		Address a = new Address();
		a.AddressId = 1;
		a.City = "Coppenhagen";
		a.Country = "Germany";
		a.Province = "Lotyssko";
		a.PostalCode = "73801";
		a.BuildingNumber = "155";
		a.ApartamentNumber = 7;
		a.Street = "Dr. Malého";
		

		Student student = new Student();
		student.Address = a;
		student.Email = "zah0089@vsb.cz";
		student.Login = "MIN0932";
		student.Phone = "+888 333 444 555";
		student.FirstName = "Pavel";
		student.LastName = "Minovsky";
		student.StudentId = 5;
		Database<Student>.Update(student);

		StudyProgram primary = new StudyProgram();
		primary.StudyProgramId = 1;
		
		StudyProgram secondary = new StudyProgram();
		secondary.StudyProgramId = 2;
		StudyProgram tetriary = new StudyProgram();
		tetriary.StudyProgramId = 3;
		
		Application application = new Application();
		application.Student = student;
		application.SecondaryProgram = secondary;
		application.PrimaryProgram = primary;
		application.TertiaryProgram = tetriary;
		
		//
		// bool worked = Database<Student>.Insert(student);
		

	}
}