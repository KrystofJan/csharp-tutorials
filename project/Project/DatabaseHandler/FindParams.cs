using System.Reflection;
using DatabaseAttrs;

namespace DatabaseHandler.DatabaseUtility;

public class FindParams {
	public static List<PropertyInfo> FindParamNames(Type t) {
		return t.GetProperties()
			.Where(p => !Attribute.IsDefined(p, typeof(IdentifierAttribute))
			            && !Attribute.IsDefined(p, typeof(InsertIgnoreAttribute)))
			.ToList();
	}

	public static string GetParamName(PropertyInfo p) {
		if (Attribute.IsDefined(p, typeof(ForeignObjectAttribute))) {
			object[] attrs = p.GetCustomAttributes(true);
			foreach (var attr in attrs) {
				if (attr is ForeignObjectAttribute) {
					ForeignObjectAttribute fo = attr as ForeignObjectAttribute;
					return fo.Name;
				}
			}
		}
		return p.Name;
	}

	public static PropertyInfo GetModelsId(Type t) {
		IEnumerable<PropertyInfo> pi = t.GetProperties()
			.Where(p => Attribute.IsDefined(p, typeof(IdentifierAttribute)));
		return pi.First();
	}
	
	public static PropertyInfo? FindParam(PropertyInfo[] parameterInfo, string paramName) {
		return parameterInfo.Where(p => p.Name == paramName).FirstOrDefault();
	}
}