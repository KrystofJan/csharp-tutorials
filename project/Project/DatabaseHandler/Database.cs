using System.Reflection;
using Microsoft.Data.Sqlite;

namespace DatabaseHandler;

public class Database {
	private static readonly Database instance = new Database();
	private string ConnectionString { get => "Data Source=database.sqlite"; }
	public string ModelsLib { get => "../../../../Models/bin/Debug/net8.0/Models.dll"; }
	
	private Database() {}

	public static Database getInstance() {
		return instance;
	}

	public void Test() {
		using (SqliteConnection connection = new SqliteConnection(ConnectionString)) {
			connection.Open();
			
			using SqliteCommand cmd = connection.CreateCommand();
			
			using SqliteCommand selectMore = connection.CreateCommand();

			selectMore.CommandText = $"Select * from {TableNames.TEST}";
			using SqliteDataReader reader = selectMore.ExecuteReader();

			while (reader.Read()) {
				Console.WriteLine(reader.GetString(reader.GetOrdinal("test")));
			}
		}
	}
}