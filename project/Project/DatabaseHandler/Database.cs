using Microsoft.Data.Sqlite;

namespace DatabaseHandler;

public class Database {
	private static readonly Database instance = new Database();
	public string ConnectionString { get => "Data Source=database.sqlite"; }
	
	private Database() {}

	public static Database getInstance() {
		return instance;
	}
	public void Test() {
		using (SqliteConnection connection = new SqliteConnection(ConnectionString)) {
			connection.Open();
			
			using SqliteCommand cmd = connection.CreateCommand();
			
			using SqliteCommand selectMore = connection.CreateCommand();
			selectMore.CommandText = "Select * from test";
			using SqliteDataReader reader = selectMore.ExecuteReader();

			while (reader.Read()) {
				Console.WriteLine(reader.GetString(reader.GetOrdinal("test")));
			}
		}
	}
}