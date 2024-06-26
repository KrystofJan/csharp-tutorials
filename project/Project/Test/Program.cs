﻿// // See https://aka.ms/new-console-template for more information
//
using DatabaseHandler;
using DatabaseHandler.DatabaseUtility.WhereCondition;
using Models;
//
// List<Student> students = Database<Student>.SelectAll();
// List<Address> Addresses = Database<Address>.SelectAll();
// List<Application> Apps = Database<Application>.SelectAll();
// List<StudyProgram> StudyPrograms = Database<StudyProgram>.SelectAll();
// List<School> schools = Database<School>.SelectAll();
//
// Condition c = Condition.AddParam("City").Like("FM").Build();
//
// List<Address> ova = Database<Address>.Select(c);
//
// Console.WriteLine("Hello world! ");
//
// Address a = new Address() {
// 	ApartamentNumber = 2,
// 	BuildingNumber = "23",
// 	City = "FM",
// 	Country = "CZ",
// 	PostalCode = "738 01",
// 	Province = "Moravskoslezsky Kraj",
// 	Street = "Sporilov"
// };
// a.AddressId = Database<Address>.Insert(a);
// a.City = "OVA";
//
// Database<Address>.Update(a);
// Database<Address>.Delete(a);
//
// Student s = Database<Student>.SelectById(1);
//
// Console.WriteLine("Hello world! ");

Student s = new Student {
	Address = new Address() { AddressId = 1},
	DateOfBirth = new DateTime(2001, 03, 28),
	Email = "jan.zahradnik@profiq.com",
	FirstName = "Jan-Krystof",
	LastName = "Zahradnik",
	Login = "ZAH0089",
	Phone = "+420 725 877 444"
};

Database<Student>.Insert(s);