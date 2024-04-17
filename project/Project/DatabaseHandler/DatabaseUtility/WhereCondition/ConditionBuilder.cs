namespace DatabaseHandler.DatabaseUtility.WhereCondition;

public class ConditionBuilder {
	private readonly Condition _condition;
	public ConditionBuilder(Condition condition) {
		_condition = condition;
	}

	private void AddToDictionary(object? value = null) {
		if (value == null) {
			return;
		}

		ValueStorage.ConditionValues[_condition.Param] = value;
	}
	
	public ConditionBuilder Like(object? value = null) {
		_condition.ParamValue = value;
		_condition.Operator = OperatorType.LIKE;
		AddToDictionary(value);
		return this;
	}    
	
	public ConditionBuilder Equals(object? value = null) {
		_condition.ParamValue = value;
		_condition.Operator = OperatorType.EQ;
		AddToDictionary(value);
		return this;
	}
	
	public ConditionBuilder NotEquals(object? value = null) {
		_condition.ParamValue = value;
		_condition.Operator = OperatorType.NEQ;
		AddToDictionary(value);
		return this;
	}
	
	
	public ConditionBuilder GreaterThan(object? value = null) {
		_condition.ParamValue = value;
		_condition.Operator = OperatorType.GT;
		AddToDictionary(value);
		return this;
	}	
	
	public ConditionBuilder LessThan(object? value = null) {
		_condition.ParamValue = value;
		_condition.Operator = OperatorType.LT;
		AddToDictionary(value);
		return this;
	}	
	
	public ConditionBuilder GreaterOrEqual(object? value = null) {
		_condition.ParamValue = value;
		_condition.Operator = OperatorType.GTEQ;
		AddToDictionary(value);
		return this;
	}	
	
	public ConditionBuilder LessOrEqual(object? value = null) {
		_condition.ParamValue = value;
		_condition.Operator = OperatorType.LTEQ;
		AddToDictionary(value);
		return this;
	}
	
	public ConditionBuilder Join(JoinOperator joinOperator, Condition condition) {
		_condition.ConditionConnection = new ConditionConnection {
			LogOp = joinOperator,
			JoinedCondition = condition
		};
		return this;
	}

	public ConditionBuilder And(Condition condition) {
		return Join(JoinOperator.AND, condition);
	}

	public ConditionBuilder Or(Condition condition) {
		return Join(JoinOperator.OR, condition);
	}

	public Condition Build() {
		return _condition;
	}
}