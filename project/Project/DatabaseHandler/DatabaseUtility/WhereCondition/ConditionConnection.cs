namespace DatabaseHandler.DatabaseUtility.WhereCondition;

public enum JoinOperator {
	AND,
	OR
}

public class ConditionConnection {
	public JoinOperator LogOp { get; set; }

	private string _logOp {
		get {
			switch (LogOp) {
				case JoinOperator.AND: {
					return "and";
				}
				case JoinOperator.OR: {
					return "or";
				}
				default: {
					return "ERROR";
				}
			}
		}
	}
	public Condition JoinedCondition { get; set; }
	
	public override string ToString() {
		return $"{_logOp} {JoinedCondition.ToString()}";
	}
}