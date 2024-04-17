using DatabaseHandler.DatabaseUtility.WhereCondition;

namespace DatabaseHandler.DatabaseUtility;


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

	public QueryBuilder Update(string tableName) {
		Result = $"Update {tableName} ";
		return this;
	}

	public QueryBuilder Set(List<string> columns) {
		Result += "SET ";
		for (int i = 0; i < columns.Count; ++i) {
			if (i + 1 >= columns.Count) {
				Result += $"{columns[i]} = @{columns[i]} ";
				continue;
			}
			Result += $"{columns[i]} = @{columns[i]}, ";
		}
		Result += " ";
		return this;
	}
	
	public QueryBuilder Values(List<string> keys) {
		Result += $"({string.Join(", ", keys)}) Values (@{string.Join(", @", keys)})";
		return this;
	}

	public QueryBuilder Where(Condition condition) {
		addStr($"WHERE {condition.ToString()}");
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