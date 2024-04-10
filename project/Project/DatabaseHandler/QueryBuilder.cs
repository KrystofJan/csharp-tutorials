using DatabaseAttrs;

namespace DatabaseHandler;

public class QueryBuilder {
	
	
	// THIS SHOULD WORK
	// TODO: Check
	private string InsertParams(ModelInfo modelInfo) {
		return string.Join(", ", modelInfo.Properties
			.ToList()
			.Where(p => !p.IsDefined(typeof(IdentifierAttribute), false))
			.Select(p =>
			{
				bool hasForeignObjectAttr = p.IsDefined(typeof(ForeignObjectAttribute), false);
				return hasForeignObjectAttr ? p.ToString() : ModelInfo.GetModelsId(modelInfo.Type.ToString());
			}));
	}
}