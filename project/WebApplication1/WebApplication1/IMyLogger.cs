namespace WebApplication1;

public interface IMyLogger {
	public  string  logFilePath { get;}
	Task Log(string ex);

}