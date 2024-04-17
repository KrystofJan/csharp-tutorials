using System.Reflection;
using DatabaseAttrs;
using DatabaseHandler.ReflectionUtility;

namespace DatabaseHandler.DatabaseUtility;

public class FindParams {
	public static List<PropertyInfo> FindParamNames(ModelInfo modelInfo) {
		return modelInfo.Properties
			.Where(p => !ModelInfo.ContainsAttr(typeof(IdentifierAttribute), p)
			            && !ModelInfo.ContainsAttr(typeof(InsertIgnoreAttribute), p))
			.ToList();
	}

	public static string GetParamName(PropertyInfo p) {
		if (ModelInfo.ContainsAttr(typeof(ForeignObjectAttribute), p)) {
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

	public static PropertyInfo GetModelsId(string modelName) {
		ModelInfo mi = new ModelInfo(modelName);

		IEnumerable<PropertyInfo> pi = mi.Properties
			.Where(p => ModelInfo.ContainsAttr(typeof(IdentifierAttribute), p));
		
		return pi.First();
	}
	
	public static PropertyInfo? FindParam(PropertyInfo[] parameterInfo, string paramName) {
		return parameterInfo.Where(p => p.Name == paramName).FirstOrDefault();
	}
}