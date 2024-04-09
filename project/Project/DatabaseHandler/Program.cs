using System;

namespace DatabaseHandler;
// Note: actual namespace depends on the project name.

internal class Program {
	
	static void Main(string[] args) {
		Database dh = Database.getInstance();
		dh.Test();
	}
}