namespace WebApplication1;

public class FirstPageMiddleware {
	private readonly RequestDelegate next;
	
	public FirstPageMiddleware(RequestDelegate next) {
		this.next = next;
	}


	public async Task Invoke(HttpContext ctx) {
		if (ctx.Request.Path == "/") {
			ctx.Response.Headers.ContentType = "text/html; charset=UTF-8";
			await ctx.Response.WriteAsync("<h1>Název stranky</h1>");
		}
		else {
			await next(ctx);
		}
	}

}