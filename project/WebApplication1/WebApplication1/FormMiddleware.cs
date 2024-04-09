namespace WebApplication1;

public class FormMiddleware {
	private readonly RequestDelegate next;

	public FormMiddleware(RequestDelegate next) {
		this.next = next;
	}

	public async Task Invoke(HttpContext ctx) {
		if (!ctx.Request.Path.StartsWithSegments("/form")) {
			await next(ctx);
			return;
		}

		ctx.Response.Headers.ContentType = "text/html; charset=URF-8";
		await ctx.Response.WriteAsync(@"
			<form method=""POST"">
				<input name=""text"" />
				<button type=""submit"">Odeslat</button>
			</form>
		");
		
	}
}