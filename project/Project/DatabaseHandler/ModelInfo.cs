using System.Reflection;
using DatabaseAttrs;

namespace DatabaseHandler;

public class ModelInfo {
	public Type Type { get; private set; }
	public string ModelName {
		get =>  Type.ToString().Split(".").Last();
	}

	public object Instance { get; private set; }
	public PropertyInfo[] Properties { get; set; }

	public MethodInfo[] Methods { get; set; }
	public Dictionary<PropertyInfo, Attribute> AttrsOnProperties = new Dictionary<PropertyInfo, Attribute>();
	
	public  ModelInfo(string modelName) {
		Type type;
		Instance = ModelInfoFactory.GetModel(modelName, out type);
		Type = type;
		Properties = Type.GetProperties();
		Methods = Type.GetMethods();
		AttrsOnProperties = ModelInfoFactory.FindAttrsOnProperties(Properties);
	}
	
	public  ModelInfo(object model) {
		Type type;
		Instance = ModelInfoFactory.GetModel(model.GetType().ToString(), out type);
		Type = type;
		Properties = Type.GetProperties();
		Methods = Type.GetMethods();
		AttrsOnProperties = ModelInfoFactory.FindAttrsOnProperties(Properties);
	}
	public  ModelInfo(Type type) {
		Instance = ModelInfoFactory.GetModel(type.ToString(), out type);
		Type = type;
		Properties = Type.GetProperties();
		Methods = Type.GetMethods();
		AttrsOnProperties = ModelInfoFactory.FindAttrsOnProperties(Properties);
	}
}