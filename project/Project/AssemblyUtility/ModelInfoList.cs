namespace DatabaseHandler.ReflectionUtility;

public class ModelInfoList {
	public static List<ModelInfo> ModelInfos {
		get => ModelInfoFactory.GetModels();
	}
}