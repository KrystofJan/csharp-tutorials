using System.Reflection;

namespace DatabaseHandler;


public class QueryBuilder {
	private string Result = "";

	private void addStr(string str) {
		Result += $" {str}";
	}
	
	public QueryBuilder SELECT(string tableName) {
		Result = $"SELECT * FROM {tableName} ";
		return this;
	}

	public QueryBuilder ADD_CONDITION(string paramName, string cmp) {
		addStr($"WHERE {paramName} {cmp} @{paramName}");
		return this;
	}

	public override string ToString() {
		return Result;
	}
}