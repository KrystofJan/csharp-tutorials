using System.Reflection;

namespace DatabaseHandler;

public class Utils {
	public static object? GetPropValue(object src, string propName) {
		PropertyInfo? propertyInfo = src.GetType().GetProperty(propName);
		if (propertyInfo == null) {
			throw new Exception($"Property {propName} could not be found!");
		}
		return propertyInfo.GetValue(src, null);
	}
}