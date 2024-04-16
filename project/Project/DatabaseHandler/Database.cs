using System.Reflection;
using DatabaseAttrs;
using Microsoft.Data.Sqlite;

namespace DatabaseHandler;

public class Database<T> {
	private static string ConnectionString {
		get => "Data Source=database.sqlite";
	}

	public static object Select(int id) {
		ModelInfo modelInfo = new ModelInfo(typeof(T));
		using SqliteConnection connection = new SqliteConnection(ConnectionString);
		connection.Open();
		using SqliteCommand cmd = connection.CreateCommand();
		QueryBuilder qb = new QueryBuilder();
		PropertyInfo identifier = FindParams.GetModelsId(modelInfo.ModelName);

		qb.Select(modelInfo.ModelName).Where(identifier.Name, "=");
		cmd.CommandText = qb.ToString();
		cmd.Parameters.AddWithValue(identifier.Name, id);

		using SqliteDataReader reader = cmd.ExecuteReader();

		while (reader.Read()) {
			foreach (var property in modelInfo.Properties) {
				string typeName = property.Name;
				bool isId = ColumnExists(reader, typeName + "Id")
				            && !reader.IsDBNull(reader.GetOrdinal(property.Name + "Id"));
				if (isId) {
					typeName += "Id";
				}

				if (!ColumnExists(reader, typeName) || reader.IsDBNull(reader.GetOrdinal(typeName))) {
					continue;
				}

				if (property.PropertyType == typeof(int)) {
					int value = reader.GetInt32(reader.GetOrdinal(typeName));
					property.SetValue(modelInfo.Instance, value);
					continue;
				}

				if (property.PropertyType == typeof(string)) {
					string value = reader.GetString(reader.GetOrdinal(typeName));
					property.SetValue(modelInfo.Instance, value);
					continue;
				}

				if (property.PropertyType == typeof(bool)) {
					string value = reader.GetString(reader.GetOrdinal(typeName));
					property.SetValue(modelInfo.Instance, value);
					continue;
				}

				if (property.PropertyType == typeof(DateTime)) {
					DateTime value = reader.GetDateTime(reader.GetOrdinal(typeName));
					property.SetValue(modelInfo.Instance, value);
					continue;
				}

				foreach (var model in ModelInfoList.ModelInfos) {
					if (property.PropertyType == model.Type) {
						int value = reader.GetInt32(reader.GetOrdinal(typeName));
						Type foreignKey = model.Type;
						MethodInfo selectMethod = typeof(Database<>)
							.MakeGenericType(foreignKey)
							.GetMethod("Select", BindingFlags.Public | BindingFlags.Static);

						model.Instance = selectMethod.Invoke(null, new object[] {value});
						if (model.Instance != null && property.PropertyType.IsAssignableFrom(model.Instance.GetType())) {
							property.SetValue(modelInfo.Instance, model.Instance);
						}
					}
				}
			}
		}
		return modelInfo.Instance;
	}
	
	public static List<object> SelectAll() {
		List<object> modelInfos = new List<object>();
		using SqliteConnection connection = new SqliteConnection(ConnectionString);
		connection.Open();
		ModelInfo mInfo = new ModelInfo(typeof(T));
		using SqliteCommand cmd = connection.CreateCommand();
		QueryBuilder qb = new QueryBuilder();

		qb.Select(mInfo.ModelName);
		cmd.CommandText = qb.ToString();

		using SqliteDataReader reader = cmd.ExecuteReader();

		while (reader.Read()) {
			ModelInfo modelInfo = new ModelInfo(typeof(T));

			foreach (var property in modelInfo.Properties) {
				string typeName = property.Name;
				bool isId = ColumnExists(reader, typeName + "Id")
				            && !reader.IsDBNull(reader.GetOrdinal(property.Name + "Id"));
				if (isId) {
					typeName += "Id";
				}

				if (!ColumnExists(reader, typeName) || reader.IsDBNull(reader.GetOrdinal(typeName))) {
					continue;
				}

				if (property.PropertyType == typeof(int)) {
					int value = reader.GetInt32(reader.GetOrdinal(typeName));
					property.SetValue(modelInfo.Instance, value);
					continue;
				}

				if (property.PropertyType == typeof(string)) {
					string value = reader.GetString(reader.GetOrdinal(typeName));
					property.SetValue(modelInfo.Instance, value);
					continue;
				}

				if (property.PropertyType == typeof(bool)) {
					string value = reader.GetString(reader.GetOrdinal(typeName));
					property.SetValue(modelInfo.Instance, value);
					continue;
				}

				if (property.PropertyType == typeof(DateTime)) {
					DateTime value = reader.GetDateTime(reader.GetOrdinal(typeName));
					property.SetValue(modelInfo.Instance, value);
					continue;
				}

				foreach (var model in ModelInfoList.ModelInfos) {
					if (property.PropertyType == model.Type) {
						int value = reader.GetInt32(reader.GetOrdinal(typeName));
						Type foreignKey = model.Type;
						MethodInfo selectMethod = typeof(Database<>)
							.MakeGenericType(foreignKey)
							.GetMethod("Select", BindingFlags.Public | BindingFlags.Static);

						model.Instance = selectMethod.Invoke(null, new object[] {value});
						if (model.Instance != null && property.PropertyType.IsAssignableFrom(model.Instance.GetType())) {
							property.SetValue(modelInfo.Instance, model.Instance);
						}
					}
				}
			}
			modelInfos.Add(modelInfo.Instance);
		}

		return modelInfos;
	}

	public static void Insert(T model) {
		using SqliteConnection connection = new SqliteConnection(ConnectionString);
		connection.Open();
		ModelInfo modelInfo = new ModelInfo(model);

		List<PropertyInfo> keys = FindParams.FindParamNames(modelInfo);
		List<string> keyString = keys.Select(p => FindParams.GetParamName(p)).ToList();
		Dictionary<PropertyInfo, string> propToKeyName = new Dictionary<PropertyInfo, string>();
		foreach (var key in keys) {
			propToKeyName[key] = FindParams.GetParamName(key);
		}

		SqliteCommand cmd = connection.CreateCommand();

		QueryBuilder qb = new QueryBuilder();
		qb.Insert(modelInfo.ModelName).Values(keyString);
		
		cmd.CommandText = qb.ToString();
		
		foreach (var prop in keys) {
			if (!ModelInfo.ContainsAttr(typeof(IdentifierAttribute), prop) 
			    && !ModelInfo.ContainsAttr(typeof(ForeignObjectAttribute), prop)) {
				object value = Utils.GetPropValue(model, prop.Name);
				cmd.Parameters.AddWithValue(prop.Name, value );
				continue;
			}
			
			PropertyInfo? foreignIdInfo = FindParams.GetModelsId(prop.PropertyType.ToString());
			object foreignObj = Utils.GetPropValue(model, prop.Name);
			object foreignId = Utils.GetPropValue(foreignObj, foreignIdInfo.Name);
			cmd.Parameters.AddWithValue(propToKeyName[prop], foreignId );
		}

		cmd.ExecuteNonQuery();
	}

	public static void Delete(T model) {
		using SqliteConnection connection = new SqliteConnection(ConnectionString);
		connection.Open();
		
		ModelInfo modelInfo = new ModelInfo(model);
		PropertyInfo id = FindParams.GetModelsId(modelInfo.ModelName);
		using SqliteTransaction transaction = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
		
		QueryBuilder qb = new QueryBuilder();
		qb.Delete(modelInfo.ModelName).Where(id.Name, "=");
		
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
		ModelInfo modelInfo = new ModelInfo(model);

		List<PropertyInfo> keys = FindParams.FindParamNames(modelInfo);
		List<string> keyString = keys.Select(p => FindParams.GetParamName(p)).ToList();
		Dictionary<PropertyInfo, string> propToKeyName = new Dictionary<PropertyInfo, string>();
		foreach (var key in keys) {
			propToKeyName[key] = FindParams.GetParamName(key);
		}

		SqliteCommand cmd = connection.CreateCommand();


	}
	
	private static bool ColumnExists(SqliteDataReader reader, string name) {
		for (int i = 0; i < reader.FieldCount; i++) {
			if (reader.GetName(i).Equals(name, StringComparison.OrdinalIgnoreCase)) {
				return true;
			}
		}

		return false;
	}
}