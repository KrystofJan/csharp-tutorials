using System.Net.Mime;

namespace WebApplication1;

public class Program {
	public static void Main(string[] args) {
		
		var builder = WebApplication.CreateBuilder(args);
		builder.Services.AddSingleton<IMyLogger, TxtLogger>();
		builder.Services.AddSingleton<ExceptionHandler>();
		
		var app = builder.Build();

		app.UseStaticFiles();
		app.UseMiddleware<BrowserAuthMiddleware>();
		app.UseMiddleware<ErrorMiddleware>();
		app.UseMiddleware<FileMiddleware>();
		app.UseMiddleware<FirstPageMiddleware>();
		app.UseMiddleware<TestMiddleware>();

		app.Run();
	}	
}
