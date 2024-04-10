using System.Reflection;
using DatabaseAttrs;

namespace DatabaseHandler;

public class ModelInfo {
	public Type Type { get; private set; }
	public object Instance { get; private set; }
	public PropertyInfo[] Properties { get; set; }

	public MethodInfo[] Methods { get; set; }
	
	public  ModelInfo(string modelName) {
		Type type;
		Instance = GetModel(modelName, out type);
		Type = type;
		Properties = Type.GetProperties();
		Methods = Type.GetMethods();
	}
	
	private object GetModel(string modelName, out Type type) {
		string modelsLibPath = Path.GetFullPath("../../../../Models/bin/Debug/net8.0/Models.dll");
		Assembly assembly = Assembly.LoadFile(modelsLibPath);
		Type[] types = assembly.GetTypes();
		if (!types.Any(type => type.Name == modelName)) {
			Console.WriteLine($"Model for this table does not exist. Table name {modelName}");	
		}
		type = assembly.GetType($"Models.{modelName}");
		
		return Activator.CreateInstance(type);
	}

	public static string  GetModelsId(string modelName) {
		ModelInfo mi = new ModelInfo(modelName);

		IEnumerable<PropertyInfo> pi = mi.Properties.ToList().Where(p => p.IsDefined(typeof(IdentifierAttribute), false));
		
		return pi.FirstOrDefault().ToString();
	}
}