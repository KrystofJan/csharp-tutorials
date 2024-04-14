using System.Reflection;
using DatabaseAttrs;
using Microsoft.Data.Sqlite;

namespace DatabaseHandler;

public class Database<T> {
	// private static readonly Database instance = new Database();
	private static string ConnectionString { get => "Data Source=database.sqlite"; }

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
			                                modelInfo.AttrsOnProperties[p] is IdentifierAttribute).First();

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
						property.SetValue(modelInfo.Instance,value);
						continue;
					}
					if (property.PropertyType == typeof(string)) {
						string value = reader.GetString(reader.GetOrdinal(typeName));
						property.SetValue(modelInfo.Instance,value);
						continue;
					}
					if (property.PropertyType == typeof(bool)) {
						string value = reader.GetString(reader.GetOrdinal(typeName));
						property.SetValue(modelInfo.Instance,value);
						continue;
					}
					if (property.PropertyType == typeof(DateTime)) {
						DateTime value = reader.GetDateTime(reader.GetOrdinal(typeName));
						property.SetValue(modelInfo.Instance,value);
						continue;
					}

					foreach (var model in ModelInfoList.ModelInfos) {
						if (property.PropertyType == model.Type) {
							PropertyInfo foreignKey = model.Properties.Where(p => model.AttrsOnProperties.ContainsKey(p) &&
								model.AttrsOnProperties[p] is IdentifierAttribute).First();
							int value = reader.GetInt32(reader.GetOrdinal(typeName));
							foreignKey.SetValue(model.Instance, value);
							property.SetValue(modelInfo.Instance, model.Instance);
						}
					}
				}
				
			}
		}

		return modelInfo.Instance;
	}

	private static bool ColumnExists(SqliteDataReader reader, string name) {
		for (int i = 0; i < reader.FieldCount; i++)
		{
			if (reader.GetName(i).Equals(name, StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
		}
		return false;
	}
}