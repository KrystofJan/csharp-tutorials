using System.Net;

namespace MyApp;

// Note: actual namespace depends on the project name.
internal class Program {



	static async Task Main(string[] args) {
		string url = "http://localhost:7961/";
		HttpClient client = new HttpClient();
		using HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, url);
		req.Content = new FormUrlEncodedContent(new Dictionary<string, string> {
			{"name", "Jan"},
			{"age", "34"}
		});
		using var res = await client.SendAsync(req);
		
		using var response = await client.GetAsync(url);

		string txt = await response.Content.ReadAsStringAsync();
		Console.WriteLine(txt);
	}
}