namespace WebApplication1;

public class ErrorMiddleware {
	private readonly RequestDelegate next;
	
	public ErrorMiddleware(RequestDelegate next) {
		this.next = next;
	}


	public async Task Invoke(HttpContext ctx, ExceptionHandler handler) {
		try {
			await next(ctx);
		}
		catch (Exception e) {
			await ctx.Response.WriteAsync("Doslo k chybe");
			await handler.Handle(e);
		}
	}
}