namespace DatabaseHandler;

public class ModelInfoList {
	public static List<ModelInfo> ModelInfos {
		get => ModelInfoFactory.GetModels();
	}
}