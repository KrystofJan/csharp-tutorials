using System.Reflection;
using DatabaseAttrs;

namespace DatabaseHandler;

public class FindParams {
	public static List<string> FindParamNames(ModelInfo modelInfo) {
		return modelInfo.Properties
			.Where(p => !modelInfo.AttrsOnProperties.ContainsKey(p) ||
			            modelInfo.AttrsOnProperties[p] is not IdentifierAttribute)
			.Select(p => {
				bool hasForeignObjectAttr = modelInfo.AttrsOnProperties.ContainsKey(p) &&
				                            modelInfo.AttrsOnProperties[p] is ForeignObjectAttribute;
				if (!hasForeignObjectAttr) {
					return p.Name;
				}
				PropertyInfo? foreignKeyInfo = ModelInfoFactory.GetModelsId(modelInfo.Type.ToString());
				return foreignKeyInfo != null ? foreignKeyInfo.Name : "";
			}).ToList();
	}

	
	public static PropertyInfo? FindForeignParam(string key, PropertyInfo param) {
		IEnumerable<Attribute> customAttributeData = param.GetCustomAttributes().ToArray();
		if (customAttributeData.Count() <= 0) {
			return null;
		}
		if (customAttributeData is not ForeignObjectAttribute) {
			return null;
		}
		
		ModelInfo modelInfo = new ModelInfo(param.PropertyType);

		foreach (var property in modelInfo.Properties) {
			if (property.Name == key) {
				return property;
			}
		}

		return null;
	}
	
	public static PropertyInfo? FindParam(PropertyInfo[] parameterInfo, string paramName) {
		return parameterInfo.Where(p => p.Name == paramName).FirstOrDefault();
	}
	
	public static List<object> FindParamValues(ModelInfo modelInfo) {
		return modelInfo.Properties
			.Where(p => !modelInfo.AttrsOnProperties.ContainsKey(p) ||
			            modelInfo.AttrsOnProperties[p] is not IdentifierAttribute)
			.Select(p => {
				bool hasForeignObjectAttr = modelInfo.AttrsOnProperties.ContainsKey(p) &&
				                            modelInfo.AttrsOnProperties[p] is ForeignObjectAttribute;
				if (!hasForeignObjectAttr) {
					return p.GetValue(modelInfo.Instance);
				}
				PropertyInfo? foreignKeyInfo = ModelInfoFactory.GetModelsId(modelInfo.Type.ToString());
				return foreignKeyInfo.GetValue(modelInfo.Instance);
			}).ToList();
	}
}