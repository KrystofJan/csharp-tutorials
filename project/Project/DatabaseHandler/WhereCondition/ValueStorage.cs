namespace DatabaseHandler.DatabaseUtility.WhereCondition;

public class ValueStorage {
	// maybe add a recover method that takes backup and sets the ConditionValues to the backup
	private static ValueStorage _instance;
	public static Dictionary<string, object> ConditionValues = new Dictionary<string, object>();

	private ValueStorage() {
	}

	public static ValueStorage getInstance() {
		if (_instance == null) {
			_instance = new ValueStorage();
		}
		return _instance;
	}

	public static void Clear() {
		ConditionValues.Clear();
	}
}