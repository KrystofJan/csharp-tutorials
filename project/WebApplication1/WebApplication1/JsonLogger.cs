using System.Text.Json;

namespace WebApplication1;

public class JsonLogger : IMyLogger {
	public string logFilePath { get => "error.json";  }

	public async Task Log(string ex) {
		await File.WriteAllTextAsync(logFilePath, JsonSerializer.Serialize(new {
			Error = ex
		}));
	}
}