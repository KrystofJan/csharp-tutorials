namespace DatabaseAttrs;
[AttributeUsage(AttributeTargets.Property)]
public class ForeignObjectAttribute : Attribute {
	public string Name { get; set; }

	public ForeignObjectAttribute(string name) {
		Name = name;
	}
}