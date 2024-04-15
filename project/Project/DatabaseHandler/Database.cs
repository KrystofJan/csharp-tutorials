using System.Reflection;
using DatabaseAttrs;
using Microsoft.Data.Sqlite;

namespace DatabaseHandler;

public class Database<T> {
	// private static readonly Database instance = new Database();
	private static string ConnectionString {
		get => "Data Source=database.sqlite";
	}

	// public static Database getInstance() {
	// 	return instance;
	// }

	public static object Select(int id) {
		ModelInfo modelInfo = new ModelInfo(typeof(T));
		using (SqliteConnection connection = new SqliteConnection(ConnectionString)) {
			connection.Open();
			using SqliteCommand cmd = connection.CreateCommand();
			QueryBuilder qb = new QueryBuilder();
			PropertyInfo identifier = modelInfo.Properties.Where(p => modelInfo.AttrsOnProperties.ContainsKey(p) &&
			                                                          modelInfo.AttrsOnProperties[p] is
				                                                          IdentifierAttribute).First();

			qb.SELECT(modelInfo.ModelName).ADD_CONDITION(identifier.Name, "=");
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
							// PropertyInfo foreignKey = model.Properties.Where(p => model.AttrsOnProperties.ContainsKey(p) &&
							// 	model.AttrsOnProperties[p] is IdentifierAttribute).First();
							int value = reader.GetInt32(reader.GetOrdinal(typeName));
							Type foreignKey = model.Type;
							MethodInfo selectMethod = typeof(Database<>)
								.MakeGenericType(foreignKey)
								.GetMethod("Select", BindingFlags.Public | BindingFlags.Static);

							model.Instance = selectMethod.Invoke(null, new object[] {value});
							if (model.Instance != null && property.PropertyType.IsAssignableFrom(model.Instance.GetType())) {
								property.SetValue(modelInfo.Instance, model.Instance);
							}
							// property.SetValue(modelInfo.Instance, model.Instance);
						}
					}
				}
			}
		}

		return modelInfo.Instance;
	}
	
	public static List<object> SelectAll() {
		List<object> modelInfos = new List<object>();
		using (SqliteConnection connection = new SqliteConnection(ConnectionString)) {
			connection.Open();
			ModelInfo mInfo = new ModelInfo(typeof(T));
			using SqliteCommand cmd = connection.CreateCommand();
			QueryBuilder qb = new QueryBuilder();

			qb.SELECT(mInfo.ModelName);
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
		}

		return modelInfos;
	}

	public static bool Insert(T model) {
		using (SqliteConnection connection = new SqliteConnection(ConnectionString)) {
			connection.Open();
			ModelInfo modelInfo = new ModelInfo(model);

			List<string> keys = FindParams.FindParamNames(modelInfo);
			modelInfo.Instance = model;
			QueryBuilder qb = new QueryBuilder();
			qb.Insert(modelInfo.ModelName).Values(keys);
			
			SqliteCommand cmd = connection.CreateCommand();
			cmd.CommandText = qb.ToString();
			bool isFinished = true;

			List<string> visitedKeys = new List<string>();
			foreach (var key in keys) {
				PropertyInfo? prop = FindParams.FindParam(modelInfo.Properties, key);

				if (prop != null && !modelInfo.AttrsOnProperties.ContainsKey(prop) ||
				    modelInfo.AttrsOnProperties[prop] is not IdentifierAttribute) {
					object value = Utils.GetPropValue(model, prop.Name);
					cmd.Parameters.AddWithValue(key, value );
					visitedKeys.Add(key);
					continue;
				}

				isFinished = false;
			}

			if (isFinished) {
				cmd.ExecuteNonQuery();
				return true;
			}

			foreach (var key in keys) {
				if (!visitedKeys.Contains(key)) {
					object finalObject = new object(); 
					foreach (var prop in modelInfo.Properties) {
						PropertyInfo? propertyInfo = FindParams.FindForeignParam(key, prop);
						
						if (propertyInfo == null) {
							continue;
						}
						
						finalObject = Utils.GetPropValue(modelInfo.Instance, propertyInfo.Name);
						object value = Utils.GetPropValue(finalObject, key);
						cmd.Parameters.AddWithValue(key, value);
						break;
					}
				}
			}
			cmd.ExecuteNonQuery();
		}
		
		return false;
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