using Dapper;
using Microsoft.Data.Sqlite;
using TestPrep.Models;
namespace TestPrep.Services;

public class Exchange {
	[Key]
	public int ExchangeId { get; set; }
	public string Name { get; set; }
	public double Ammount { get; set; }
	public string? Email { get; set; }	
}


public class SaveDataExchange {
	public SaveDataExchange() {
	}

	public void Save(IndexForm indexForm) {
		string connStr = "Data Source=/home/zahry/school/CSharp/testprep/TestPrep/identifier.sqlite";
		Exchange exchange = new Exchange {
			Ammount = indexForm.Amount,
			Email = indexForm.Email,
			Name = indexForm.Name
		};
		SimpleCRUD.SetDialect(SimpleCRUD.Dialect.SQLite);
		using (SqliteConnection conn = new SqliteConnection(connStr)) {
			int id = conn.Execute("insert into Exchange (Name, Email, Ammount) values (@Name, @Email, @Ammount)",
				exchange);
		}
		Console.WriteLine("sadasd");
	}

	public List<Exchange> Get() {
		
		string connStr = "Data Source=/home/zahry/school/CSharp/testprep/TestPrep/identifier.sqlite";
		List<Exchange> exchange = new List<Exchange>();
		SimpleCRUD.SetDialect(SimpleCRUD.Dialect.SQLite);
		using (SqliteConnection conn = new SqliteConnection(connStr)) {
			exchange = conn.Query<Exchange>(@"Select * from Exchange").ToList();
		}
		return exchange;
	}
}