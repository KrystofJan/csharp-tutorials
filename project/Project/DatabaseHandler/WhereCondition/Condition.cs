namespace DatabaseHandler.DatabaseUtility.WhereCondition;


public enum OperatorType {
	EQ,
	NEQ,
	LIKE,
	GT,
	LT,
	GTEQ,
	LTEQ
}

public class Condition {
	public ValueStorage ValueStorage = ValueStorage.getInstance();
	public string Param { get; set; }
	public object? ParamValue { get; set; }
	public OperatorType Operator { get; set; }
	private string _operator {
		get {
			switch (Operator) {
				case OperatorType.EQ: {
					return "=";
				}
				case OperatorType.NEQ: {
					return "!=";
				}
				case OperatorType.LIKE: {
					return "LIKE";
				}
				case OperatorType.GT: {
					return ">";
				}
				case OperatorType.LT: {
					return "<";
				}
				case OperatorType.GTEQ: {
					return ">=";
				}
				case OperatorType.LTEQ: {
					return "<=";
				}
				default: {
					return "ERROR";
				}
			}	
		}
	}
	
	public static ConditionBuilder AddParam(string param) {
		return new ConditionBuilder(new Condition { Param = param });
	}
	public ConditionConnection? ConditionConnection { get; set; }
	
	public override string ToString() {
		string? cond = ConditionConnection == null ? null : ConditionConnection.ToString();
		return $"{Param} {_operator} @{Param} {cond ?? ""}";
	}

	public Dictionary<string, object> GetValues() {
		Dictionary<string, object> tmp = new Dictionary<string, object>();
		foreach (var kvp in ValueStorage.ConditionValues) {
			tmp[kvp.Key] = kvp.Value;
		}
		ValueStorage.Clear();
		return tmp;
	}
}
