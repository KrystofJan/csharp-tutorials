using System.Reflection;

namespace DatabaseHandler;


public class QueryBuilder {
	private string Result = "";

	private void addStr(string str) {
		Result += $" {str}";
	}
	
	public QueryBuilder Select(string tableName) {
		Result = $"SELECT * FROM {tableName} ";
		return this;
	}

	public QueryBuilder Insert(string tableName) {
		Result = $"Insert into {tableName} ";
		return this;
	}

	public QueryBuilder Delete(string tableName) {
		Result = $"Delete from {tableName} ";
		return this;
	}

	public QueryBuilder Values(List<string> keys) {
		Result += $"({string.Join(", ", keys)}) Values (@{string.Join(", @", keys)})";
		return this;
	}

	public QueryBuilder Where(string paramName, string cmp) {
		addStr($"WHERE {paramName} {cmp} @{paramName}");
		return this;
	}

	public override string ToString() {
		return Result;
	}
}