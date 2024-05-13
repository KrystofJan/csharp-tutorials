using System.ComponentModel.DataAnnotations;
using TestPrep.Services;
namespace TestPrep.Models;

public class IndexForm {
	[Required]
	public string Name { get; set; }
	
	[EmailAddress]
	public string? Email { get; set; }
	[Required]
	public string ExchangeRateName { get; set; }

	public ExchangeRate? ExchangeRate { get; set; }

	[Required]
	[Range(0, int.MaxValue)]
	public int Amount { get; set; }

}