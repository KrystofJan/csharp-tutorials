namespace DatabaseHandler;

public class Utils {
	public static object GetPropValue(object src, string propName)
	{
		return src.GetType().GetProperty(propName).GetValue(src, null);
	}
}