using System;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using MailKit.Net.Smtp;
using MimeKit;

namespace MyApp;

// Note: actual namespace depends on the project name.
internal class Program {
	static async Task Main(string[] args) {
		HttpListener listener = new HttpListener();
		listener.Prefixes.Add("http://localhost:7961/");

		listener.Start();

		while (true) {
			HttpListenerContext ctx = listener.GetContext();
			using StreamReader sr = new StreamReader(ctx.Request.InputStream);
			string txt = sr.ReadToEnd();
			using (StreamWriter sw = new StreamWriter(ctx.Response.OutputStream)) {
				if (ctx.Request.RawUrl.EndsWith("test")) {
					sw.WriteLine("Test");
				}
				else {
					sw.WriteLine("Ahoj");
				}
			}

			ctx.Response.OutputStream.Close();
		}
		// HttpListenerContext ctx = listener.GetContext();
		// ctx.Response.OutputStream.WriteByte((byte) 'A');
	}

	static async Task Mail(string[] args) {
		using MimeMessage msg = new MimeMessage();
		msg.Subject = "testovaci email";
		msg.Sender = new MailboxAddress("Csharp", "atnet2019@seznam.cz");
		msg.To.Add(new MailboxAddress("Já", "zah0089@vsb.cz"));
		BodyBuilder builder = new BodyBuilder();
		builder.HtmlBody = "<h1>Hi from c#</h1>";
		builder.TextBody = "Hi from c# in plain text";
		msg.Body = builder.ToMessageBody();

		using SmtpClient smtpClient = new SmtpClient();
		await smtpClient.ConnectAsync("smtp.seznam.cz", 465);
		await smtpClient.AuthenticateAsync("atnet2019@seznam.cz", "Csharp2023");

		await smtpClient.SendAsync(msg);
		await smtpClient.DisconnectAsync(true);
	}


	static async Task Http(string[] args) {
		string url
			= "https://www.7timer.info/bin/astro.php?lon=18.160005506399536&lat=49.831015379859586&ac=0&unit=metric&output=json&tzshift=0";
		using HttpClient client = new HttpClient();

		using HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Post, url);
		msg.Headers.Authorization = AuthenticationHeaderValue.Parse("Bearer edasdasd");
		msg.Headers.Add("Nazev", "Hodnota");

		msg.Content = new StringContent("{\"a\": 1}", Encoding.UTF8, "application/json");

		// msg.Content = new FormUrlEncodedContent(new Dictionary<string,string>() {
		// 	{"A", "a"},
		// 	{"B", "a"}
		// });

		HttpResponseMessage response = await client.SendAsync(msg); //await client.GetAsync(url);

		if (response.StatusCode == HttpStatusCode.OK) {
			// using Stream st = response.Content.ReadAsStream();
			// using FileStream fs = new FileStream("data.json", FileMode.Create);
			// await st.CopyToAsync(fs);
			string str = await response.Content.ReadAsStringAsync();
			Console.WriteLine(str);
			Data data = JsonSerializer.Deserialize<Data>(response.Content.ReadAsStream());
			foreach (Dataseries d in data.dataseries) {
				Console.WriteLine($"{d.temp2m} - {d.timepoint}");
			}
		}
	}
}