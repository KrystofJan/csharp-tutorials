using System;
using Models;

namespace DatabaseHandler;
// Note: actual namespace depends on the project name.

internal class Program {
	
	static void Main(string[] args) {
		object addr = Database<Student>.Select(1);
		
		Console.WriteLine();
	}
}