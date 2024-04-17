using System.Reflection;

namespace DatabaseHandler.ReflectionUtility;

public class ModelInfoFactory {
	
	
	public static object GetModel(string modelName, out Type type) {
		string modelsLibPath = Path.GetFullPath("../../../../Models/bin/Debug/net8.0/Models.dll");
		Assembly assembly = Assembly.LoadFile(modelsLibPath);
		Type[] types = assembly.GetTypes();
		if (types.Any(type => type.Name != modelName)) {
			// Console.WriteLine($"Model for this table does not exist. Table name {modelName}");	
		}

		if (modelName.Split(".")[0] == "Models") {
			type = assembly.GetType($"{modelName}");
		}
		else {
			type = assembly.GetType($"Models.{modelName}");
		}

		return Activator.CreateInstance(type);
	}
	
	public static List<ModelInfo> GetModels() {
		string modelsLibPath = Path.GetFullPath("../../../../Models/bin/Debug/net8.0/Models.dll");
		Assembly assembly = Assembly.LoadFile(modelsLibPath);
		Type[] types = assembly.GetTypes();

		List<ModelInfo> modelInfos = new List<ModelInfo>();
		foreach (var type in types) {
			ModelInfo modelInfo = new ModelInfo(type);
			modelInfos.Add(modelInfo);
		}
		return modelInfos;
	}
}