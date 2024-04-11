using System.Reflection;
using DatabaseAttrs;

namespace DatabaseHandler;

public class QueryBuilder {
	
	

	public static string InsertParams(ModelInfo modelInfo) {
		return string.Join(", ", modelInfo.Properties
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
			}));
	}
}