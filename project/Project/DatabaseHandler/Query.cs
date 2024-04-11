using System.Reflection;

namespace DatabaseHandler;


public delegate bool Condition();

public class SelectQuery {
	private string SelectStr { get; set; }
	private string FromStr { get; set; }
	private string WhereStr { get; set; }

	private object PrimaryObject { get; set; }

	public SelectQuery From(object Table) {
		PrimaryObject = Table;
		ModelInfo mi = new ModelInfo(Table);
		FromStr = $"From {mi.ModelName} ";
		return this;
	}

	public SelectQuery InnerJoin(object Table) {
		ModelInfo mi = new ModelInfo(Table);
		// FromStr = $" inner join {mi.ModelName} on {ModelInfoFactory.}"
		return this;
	}

	public string CreateStringFromQuery() {
		return $"{SelectStr}c{FromStr} {WhereStr}";
	}
}