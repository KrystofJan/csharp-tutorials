using System.ComponentModel.DataAnnotations;

namespace ASPNET_MVC.Forms;

public class CartForm {
	[Required]
	[Display(Name = "Jméno")]
	public string Name { get; set; }

	[Required]
	[EmailAddress]
	public string Email { get; set; }

	// svoje musi dedit validation Attribute
	[Required]
	[Range(10, 100)]
	[Display(Name = "Věk")]
	public int? Age { get; set; }
}