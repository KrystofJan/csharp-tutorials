using System.Reflection;
using System.Runtime.CompilerServices;
using DatabaseAttrs;

namespace DatabaseHandler;

public class ModelInfo {
	public Type Type { get; private set; }
	public string ModelName {
		get =>  Type.ToString().Split(".").Last();
	}

	public object Instance { get; set; }
	public PropertyInfo[] Properties { get; set; }

	public MethodInfo[] Methods { get; set; }
	
	public  ModelInfo(string modelName) {
		Type type;
		Instance = ModelInfoFactory.GetModel(modelName, out type);
		Type = type;
		Properties = Type.GetProperties();
		Methods = Type.GetMethods();
	}
	
	public  ModelInfo(object model) {
		Type type;
		object tmp = ModelInfoFactory.GetModel(model.GetType().ToString(), out type);
		Instance = model;
		Type = type;
		Properties = Type.GetProperties();
		Methods = Type.GetMethods();
	}
	public  ModelInfo(Type type) {
		Instance = ModelInfoFactory.GetModel(type.ToString(), out type);
		Type = type;
		Properties = Type.GetProperties();
		Methods = Type.GetMethods();
	}

	public static bool ContainsAttr(Type attribute, PropertyInfo propertyInfo) {
		return Attribute.IsDefined(propertyInfo, attribute);
	}
}