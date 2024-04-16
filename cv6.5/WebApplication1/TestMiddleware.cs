using System.Dynamic;

namespace WebApplication1;

public class TestMiddleware {
	private readonly RequestDelegate next;

	public TestMiddleware(RequestDelegate next) {
		this.next = next;
	}

	public async Task Invoke(HttpContext ctx) {
		if (ctx.Request.Path == "/test") {
			
			ctx.Response.Headers.ContentType = "text/html; charset=UTF-8";
			await ctx.Response.WriteAsync("<h1>Testovací stranky</h1>");
		}
		else {
			await next(ctx);
		}
	}
}