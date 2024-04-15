using System;
using System.Net.Sockets;
using Dapper;
using Microsoft.Data.Sqlite;

namespace PraceSDB;

public class Customer {
	[Key] // reknu mu, ze tohle je primarni klics
	public int Id { get; set; }

	public string Name { get; set; }
	public string Address { get; set; }
}

internal class Program {
	static void Main(string[] args) {
		string connectionString = "Data Source=janousekcviko5.db;";
		SimpleCRUD.SetDialect(SimpleCRUD.Dialect.SQLite);

		using (SqliteConnection connection = new SqliteConnection(connectionString)) {
			// SqliteTransaction transaction = connection.BeginTransaction();
			// connection.Execute(
			// 	sql: "Insert Into Customer (Name, Address) VALUES (@Name, @Address)",
			// 	param: new Customer { Name= "Ean Obchodnik", Address = "Praha"}
			// );
			// connection.Execute(
			// 	sql: "Insert Into Customer (Name, Address) VALUES (@name, @address)",
			// 	param: new { name= "Ean Obchodnik", address = "Praha" }
			// );

			long count = connection.ExecuteScalar<long>(
				sql: "Select count(*) from customer" //,
				// transaction: transaction
			);
			// transaction.Commit();

			Console.WriteLine(count);

			IEnumerable<Customer> customers = connection.Query<Customer>("Select * from customer");
			foreach (var customer in customers) {
				Console.WriteLine($"{customer.Id}: {customer.Name} - {customer.Address}");
			}

			int? id = connection.Insert(new Customer() {Name = "aaa", Address = "21 street"});

			connection.Open();
			using SqliteTransaction tran = connection.BeginTransaction();
			Customer cust = connection.Get<Customer>(id, tran);
			cust.Address = "Praha";
			connection.Update(cust);
			tran.Commit();

			connection.Delete(cust);
		}

		// cmd.ExecuteNonQuery();
		// connection.Close();
	}

	static void SQLiteWorkFlow() {
		string connectionString = "Data Source=janousekcviko5.db;";
		using (SqliteConnection connection = new SqliteConnection(connectionString)) {
			connection.Open();

			// using SqliteCommand cmd = connection.CreateCommand();
			// // SqliteCommand cmd = new SqliteCommand();
			// // cmd.Connection = connection;
			// cmd.CommandText = File.ReadAllText("database-create.sql");
			// cmd.ExecuteNonQuery();

			using SqliteCommand cmd = connection.CreateCommand();
			// string name = "Dal Dalousek";
			// string city = "Stredni Zadni";
			//
			// cmd.CommandText = "Insert into Customer(name, address) values(@name, @address)";
			// cmd.Parameters.Add(new SqliteParameter {
			// 	ParameterName = "name",
			// 	Value = DBNull.Value,
			// 	DbType = System.Data.DbType.String
			// });
			// cmd.Parameters.AddWithValue("address", city);
			// cmd.ExecuteNonQuery();

			using SqliteCommand select = connection.CreateCommand();
			select.CommandText = "Select Count(*) from Customer";
			long count = (long) select.ExecuteScalar();
			Console.WriteLine(count);

			using SqliteCommand selectMore = connection.CreateCommand();
			selectMore.CommandText = "Select * from Customer";
			using SqliteDataReader reader = selectMore.ExecuteReader();

			while (reader.Read()) {
				int id = reader.GetInt32(reader.GetOrdinal("Id"));
				string namee = reader.GetString(reader.GetOrdinal("Name"));
				string? address;
				if (reader.IsDBNull(reader.GetOrdinal("Address"))) {
					address = reader.GetString(reader.GetOrdinal("Address"));
				}

				// Console.WriteLine($"{id}: {namee} is from {(address)? address: ""}"); // nejak to preved
			}

			// SqliteCommand orderInsert = connection.CreateCommand();
			// orderInsert.CommandText = "Insert into [Order] (CustomerId, Product, Price) values (@customerId, @product, @price)"; // [] kdyz je tablename keyword v db
			// orderInsert.Parameters.AddWithValue("customerId", 2);
			// orderInsert.Parameters.AddWithValue("product", "Produktik2");
			// orderInsert.Parameters.AddWithValue("price", 421);

			// orderInsert.ExecuteNonQuery();

			using SqliteTransaction transaction = connection.BeginTransaction(); // zacnu transakci
			for (int i = 0; i < 2; ++i) {
				SqliteCommand orderInsert = connection.CreateCommand();
				orderInsert.Transaction = transaction; // nastavim transakci
				orderInsert.CommandText
					= "Insert into [Order] (CustomerId, Product, Price) values (@customerId, @product, @price)"; // [] kdyz je tablename keyword v db
				orderInsert.Parameters.AddWithValue("customerId", i + i);
				orderInsert.Parameters.AddWithValue("product", "Produktik2");
				orderInsert.Parameters.AddWithValue("price", 421 * i);

				orderInsert.ExecuteNonQuery();
			}

			transaction.Commit();
		}
	}
}