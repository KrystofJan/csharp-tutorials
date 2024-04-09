namespace WebApplication1;

public class TxtLogger : IMyLogger {
	public string logFilePath { get => "error.txt"; }

	public async Task Log(string ex) {
		await File.WriteAllTextAsync(logFilePath, ex);
	}
}