using Microsoft.AspNetCore.Http.HttpResults;

namespace WebApplication1;

public class FileMiddleware {
	private readonly RequestDelegate next;
	
	public FileMiddleware(RequestDelegate next) {
		this.next = next;
	}

	
	public async Task Invoke(HttpContext ctx) {
		if (ctx.Request.Path.Value.EndsWith(".png")) {
			string storage = @"C:\Users\zahry\CSharp2-tutorials\Storage";
			string filePath = Path.Combine(storage, ctx.Request.Path.Value.TrimStart('/'));
			if (File.Exists(filePath)) {
				ctx.Response.ContentType = "image/png";
				await ctx.Response.SendFileAsync(filePath);
				return;
			}
			ctx.Response.StatusCode = 404;
			return;
		}
		
		await next(ctx);
	}
}