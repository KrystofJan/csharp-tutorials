using System.Reflection.Metadata;

namespace WebApplication1;

public class ExceptionHandler {
	public IMyLogger Logger { get; set; }

	public ExceptionHandler(IMyLogger logger) {
		Logger = logger;
	}

	public async Task Handle(Exception exception) {
		await Logger.Log(exception.Message + exception.StackTrace);
	}
}