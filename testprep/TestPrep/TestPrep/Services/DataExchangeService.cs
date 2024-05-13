using System.Text.Json;
namespace TestPrep.Services;

public class DataExchange {
	public Dictionary<string, ExchangeRateJson> kurzy { get; set; }
}

public class ExchangeRateJson {
	public double dev_stred { get; set; }
}

public class ExchangeRate {
	public string Name { get; set; }
	public double Value { get; set; }
}

public class DataExchangeService {
	public List<ExchangeRate> _exchangeRates { get; set; }
	public DataExchangeService() {
		_exchangeRates = new List<ExchangeRate>();
	}

	public async Task<List<ExchangeRate>> GetDataExchange() {
		HttpClient client = new HttpClient();
		string result = "";
		DataExchange jsonos = null;
		HttpResponseMessage response = await client.GetAsync("https://data.kurzy.cz/json/meny/b[1].json");
		if (response.IsSuccessStatusCode) {
			result = await response.Content.ReadAsStringAsync();
			jsonos = JsonSerializer.Deserialize<DataExchange>(response.Content.ReadAsStream());
		}
		List<ExchangeRate> exchangeRates = new List<ExchangeRate>();

		foreach (var kurz in jsonos.kurzy) {
			exchangeRates.Add(new ExchangeRate{Name = kurz.Key, Value = kurz.Value.dev_stred});
			_exchangeRates.Add(new ExchangeRate{Name = kurz.Key, Value = kurz.Value.dev_stred});
		}

		return exchangeRates;
	}	
}