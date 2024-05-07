using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Authorization;
using Models;
using WebApp.ValidationAttrs;

namespace WebApp.Models;

public class ApplicationForm {
	// Student
	[Required(ErrorMessage = "Login musí být vyplněn")]
	[Display(Name = "Login")]
	public string Login { get; set; }
	
	[Required(ErrorMessage = "Křestní jméno musí být vyplněno")]
	[Display(Name = "Křestní jméno")]
	public string FirstName { get; set; }
	
	[Required(ErrorMessage = "Příjmení musí být vyplněno")]
	[Display(Name = "Příjmení")]
	public string LastName { get; set; }
	
	[Required(ErrorMessage = "Datum narození musí být vyplněno")] 
	[Display(Name = "Datum narození")]
	[AgeRequiredAttribure(14)]
	[DataType(DataType.Date)]
	[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
	public DateTime DateOfBirth { get; set; }
	
    [Required(ErrorMessage = "Telefonní číslo musí být vyplněno")]
    [Phone(ErrorMessage = "Telefonní číslo musí mít správný formát")]
    [Display(Name = "Telefonní číslo")]
	public string Phone{ get; set; }
	
    [Required(ErrorMessage = "Email musí být vyplněný")]
    [EmailAddress(ErrorMessage = "Email musí mít správný formát")]
    [Display(Name = "Emailová adresa")]
	public string Email { get; set; }
	
	// Address

    [Required(ErrorMessage = "Země musí být vyplněna")]
    [Display(Name = "Země")]
	public string Country { get; set; }
	
    [Required(ErrorMessage = "Kraj musí být vyplněný")]
    [Display(Name = "Kraj (provincie, stát...)")]
	public string Province { get; set; }
	
    [Required(ErrorMessage = "Ulice musí výt vyplněna")]
    [Display(Name = "Ulice")]
	public string Street { get; set; }
	
    [Required(ErrorMessage = "Město musí být vyplněno")]
    [Display(Name = "Město")]
	public string City { get; set; }
	
    [Required(ErrorMessage = "Číslo popisné musí být vyplněno")]
    [Display(Name = "Číslo popisné")]
	public string BuildingNumber { get; set; }
	
    [Display(Name = "Číslo bytu")]
	public int?  ApartamentNumber { get; set; }
	
    [Required(ErrorMessage = "PSČ musí být vyplněno")]
    [Display(Name = "PSČ")]
	public string PostalCode { get; set; }
	
	[Display(Name="Najdi studijní program")]
	public string SchoolSearchTerm { get; set; }

	// Studijni programy
	[Required(ErrorMessage = "Musí být vybrán aspoň jeden studijní program")]
	[Range(1, int.MaxValue, ErrorMessage = "Musí být vybrán aspoň jeden studijní program")]
	public int PrimaryProgramId { get; set; }
	public int? SecondaryProgramId { get; set; }
	public int? TertiaryProgramId { get; set; }
}