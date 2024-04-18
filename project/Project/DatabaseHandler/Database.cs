using System.Reflection;
using DatabaseAttrs;
using DatabaseHandler.DatabaseUtility;
using DatabaseHandler.DatabaseUtility.WhereCondition;
using DatabaseHandler.ReflectionUtility;
using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic.CompilerServices;

namespace DatabaseHandler;

public class Database<T> {
	private static string ConnectionString {
		get => "Data Source=../../../../database.sqlite";
	}

	private static List<T> Read(SqliteDataReader reader) {
		List<T> result = new List<T>();
		while (reader.Read()) {
			T targetModel = (T) typeof(T).GetConstructor(new Type[0]).Invoke(null);
			foreach (var property in typeof(T).GetProperties()) {
				string typeName = property.Name;

				if (property.PropertyType == typeof(int)) {
					int value = reader.GetInt32(reader.GetOrdinal(typeName));
					property.SetValue(targetModel, value);
					continue;
				}

				if (property.PropertyType == typeof(string)) {
					string value = reader.GetString(reader.GetOrdinal(typeName));
					property.SetValue(targetModel, value);
					continue;
				}

				if (property.PropertyType == typeof(bool)) {
					string value = reader.GetString(reader.GetOrdinal(typeName));
					property.SetValue(targetModel, value);
					continue;
				}

				if (property.PropertyType == typeof(DateTime)) {
					DateTime value = reader.GetDateTime(reader.GetOrdinal(typeName));
					property.SetValue(targetModel, value);
					continue;
				}

				int idValue = reader.GetInt32(reader.GetOrdinal(typeName + "Id"));
				MethodInfo? selectMethod = typeof(Database<>).MakeGenericType(property.PropertyType)
					.GetMethod("SelectById", BindingFlags.Public | BindingFlags.Static);

				if (selectMethod == null) {
					throw new Exception("Select method does not exist");
				}
					
				dynamic foreignObj = selectMethod.Invoke(null, new object[] {idValue});
				if (foreignObj != null &&
				    property.PropertyType.IsAssignableFrom(foreignObj.GetType())) {
					property.SetValue(targetModel, foreignObj);
				}
			}
			result.Add(targetModel);
		}

		return result;
	}
	
	public static List<T> Select(Condition condition) {
		T targetModel = (T) typeof(T).GetConstructor(new Type[0]).Invoke(null);
		using SqliteConnection connection = new SqliteConnection(ConnectionString);
		connection.Open();
		using SqliteCommand cmd = connection.CreateCommand();
		QueryBuilder qb = new QueryBuilder();
		PropertyInfo identifier = FindParams.GetModelsId(typeof(T));
		qb.Select(typeof(T).Name).Where(condition);
		cmd.CommandText = qb.ToString();
		
		Dictionary<string, object> values = condition.GetValues();
		foreach (var kvp in values) {
			cmd.Parameters.AddWithValue(kvp.Key, kvp.Value);
		}
		
		using SqliteDataReader reader = cmd.ExecuteReader();

		return Read(reader);
	}

	public static T SelectById(int id) {
		using SqliteConnection connection = new SqliteConnection(ConnectionString);
		connection.Open();
		using SqliteCommand cmd = connection.CreateCommand();
		QueryBuilder qb = new QueryBuilder();
		PropertyInfo identifier = FindParams.GetModelsId(typeof(T));
		qb.Select(typeof(T).Name);
	
		qb.Where(identifier.Name, "=");

		cmd.CommandText = qb.ToString();

		cmd.Parameters.AddWithValue(identifier.Name, id);
		
		using SqliteDataReader reader = cmd.ExecuteReader();

		return Read(reader)[0];
	}

	// TODO maybe refactor
	public static List<T> SelectAll(Condition? condition = null) {
		using SqliteConnection connection = new SqliteConnection(ConnectionString);
		connection.Open();
		using SqliteCommand cmd = connection.CreateCommand();
		
		QueryBuilder qb = new QueryBuilder();
		qb.Select(typeof(T).Name);
		if (condition != null) {
			qb.Where(condition);
		}
		cmd.CommandText = qb.ToString();

		if (condition != null) {
			Dictionary<string, object> values = condition.GetValues();
			foreach (var kvp in values) {
				cmd.Parameters.AddWithValue(kvp.Key, kvp.Value);
			}
		}

		using SqliteDataReader reader = cmd.ExecuteReader();

		return Read(reader);
	}

	public static int Insert(T model) {
		using SqliteConnection connection = new SqliteConnection(ConnectionString);
		connection.Open();

		List<PropertyInfo> keys = FindParams.FindParamNames(typeof(T));
		List<string> keyString = keys.Select(p => FindParams.GetParamName(p)).ToList();
		Dictionary<PropertyInfo, string> propToKeyName = new Dictionary<PropertyInfo, string>();
		foreach (var key in keys) {
			propToKeyName[key] = FindParams.GetParamName(key);
		}

		SqliteCommand cmd = connection.CreateCommand();

		QueryBuilder qb = new QueryBuilder();
		qb.Insert(typeof(T).Name).Values(keyString);

		cmd.CommandText = qb.ToString();

		foreach (var prop in keys) {
			if (!ModelInfo.ContainsAttr(typeof(IdentifierAttribute), prop)
			    && !ModelInfo.ContainsAttr(typeof(ForeignObjectAttribute), prop)) {
				object value = Utils.GetPropValue(model, prop.Name);
				cmd.Parameters.AddWithValue(prop.Name, value);
				continue;
			}

			PropertyInfo? foreignIdInfo = FindParams.GetModelsId(prop.PropertyType);
			object foreignObj = Utils.GetPropValue(model, prop.Name);
			object foreignId = Utils.GetPropValue(foreignObj, foreignIdInfo.Name);
			cmd.Parameters.AddWithValue(propToKeyName[prop], foreignId);
		}

		cmd.ExecuteNonQuery();

		SqliteCommand fetchId = connection.CreateCommand();
		fetchId.CommandText = "SELECT last_insert_rowid()";
		int lastID = Convert.ToInt32(fetchId.ExecuteScalar());
		return lastID;
	}

	public static void Delete(T model) {
		using SqliteConnection connection = new SqliteConnection(ConnectionString);
		connection.Open();
		
		PropertyInfo id = FindParams.GetModelsId(typeof(T));
		using SqliteTransaction transaction = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

		QueryBuilder qb = new QueryBuilder();
		qb.Delete(typeof(T).Name).Where(id.Name, "=");

		SqliteCommand cmd = connection.CreateCommand();
		cmd.CommandText = qb.ToString();
		object idVal = Utils.GetPropValue(model, id.Name);
		cmd.Parameters.AddWithValue(id.Name, idVal);

		cmd.ExecuteNonQuery();

		transaction.Commit();
	}

	public static void Update(T model) {
		using SqliteConnection connection = new SqliteConnection(ConnectionString);
		connection.Open();

		PropertyInfo identifier = FindParams.GetModelsId(typeof(T));
		List<PropertyInfo> keys = FindParams.FindParamNames(typeof(T));
		List<string> keyString = keys.Select(p => FindParams.GetParamName(p)).ToList();
		Dictionary<PropertyInfo, string> propToKeyName = new Dictionary<PropertyInfo, string>();
		foreach (var key in keys) {
			propToKeyName[key] = FindParams.GetParamName(key);
		}

		SqliteCommand cmd = connection.CreateCommand();

		object idVal = Utils.GetPropValue(model, identifier.Name);
		QueryBuilder qb = new QueryBuilder();
		qb.Update(typeof(T).Name).Set(keyString).Where(identifier.Name, "=");
		cmd.CommandText = qb.ToString();
		cmd.Parameters.AddWithValue(identifier.Name, idVal);
		foreach (var prop in keys) {
			if (!ModelInfo.ContainsAttr(typeof(IdentifierAttribute), prop)
			    && !ModelInfo.ContainsAttr(typeof(ForeignObjectAttribute), prop)) {
				object value = Utils.GetPropValue(model, prop.Name);
				cmd.Parameters.AddWithValue(prop.Name, value);
				continue;
			}

			PropertyInfo? foreignIdInfo = FindParams.GetModelsId(prop.PropertyType);
			object foreignObj = Utils.GetPropValue(model, prop.Name);
			object foreignId = Utils.GetPropValue(foreignObj, foreignIdInfo.Name);
			cmd.Parameters.AddWithValue(propToKeyName[prop], foreignId);
		}

		cmd.ExecuteNonQuery();
	}
}