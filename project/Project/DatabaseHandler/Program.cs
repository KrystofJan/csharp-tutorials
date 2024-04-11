using System;

namespace DatabaseHandler;
// Note: actual namespace depends on the project name.

internal class Program {
	
	static void Main(string[] args) {
		Database dh = Database.getInstance();
		dh.Test();
		
		ModelInfo modelInfo = new ModelInfo(TableNames.STUDY_PROGRAM);
		object model = modelInfo.Instance;
		SelectQuery t = new SelectQuery().From(model);

		string query = QueryBuilder.InsertParams(modelInfo);
		Console.WriteLine(query);
		Console.WriteLine();
		
	}
}