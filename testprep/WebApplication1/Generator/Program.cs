using System;
using System.Xml.Serialization;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Generator;

public class Pub {
	[Key]
	public int Pubid { get; set; }
	public string Name { get; set; }
	public int MaxCapacity { get; set; }
	public string OpenedSinceOnWeekdays { get; set; }
	public string OpenedToOnWeekDays { get; set; }
	public string OpenedSinceOnWeekends { get; set; }
	public string OpenedToOnWeekends { get; set; }

	public override string ToString() {
		return $"{Name} - {Pubid}";
	}
}

internal class Program {

	static void Main(string[] args) {
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Pub>));
		using StreamReader fs = new StreamReader("Pubs.xml");

		List<Pub> pubs = (List<Pub>)xmlSerializer.Deserialize(fs);
		Console.WriteLine(string.Join(", ", pubs));

		string connectionString = "Data Source=/home/zahry/school/pjp/cv1/WebApplication1/Data.sqlite";
		SimpleCRUD.SetDialect(SimpleCRUD.Dialect.SQLite);
		using (SqliteConnection connection = new SqliteConnection(connectionString)) {
			foreach (Pub pub in pubs) {
				long count = connection.Execute(
					@"insert into 
					Pub(Name, MaxCapacity, OpenedSinceOnWeekdays, OpenedToOnWeekdays, 
						OpenedSinceOnWeekends, OpenedToOnWeekends) 
					values (@Name, @MaxCapacity, @OpenedSinceOnWeekdays, @OpenedToOnWeekdays, 
						@OpenedSinceOnWeekends, @OpenedToOnWeekends)",
					pub);

				Console.WriteLine(count);
			}
		}

	}
}