using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Models;
using WebApp.ValidationAttrs;

namespace WebApp.Models;

public class ApplicationForm {
	// Student
	[Required]
	[Display(Name = "Login")]
	public string Login { get; set; }
	
	[Required]
	[Display(Name = "Křestní jméno")]
	public string FirstName { get; set; }
	
	[Required]
	[Display(Name = "Příjmení")]
	public string LastName { get; set; }
	
	[Required] 
	[Display(Name = "Datum narození")]
	[AgeRequiredAttribure(14)]
	[DataType(DataType.Date)]
	[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
	public DateTime DateOfBirth { get; set; }
	
    [Required]
    [Phone]
    [Display(Name = "Telefonní číslo")]
	public string Phone{ get; set; }
	
    [Required]
    [EmailAddress]
    [Display(Name = "Emailová adresa")]
	public string Email { get; set; }
	
	// Address

    [Required]
    [Display(Name = "Země")]
	public string Country { get; set; }
	
    [Required]
    [Display(Name = "Kraj (provincie, stát...)")]
	public string Province { get; set; }
	
    [Required]
    [Display(Name = "Ulice")]
	public string Street { get; set; }
	
    [Required]
    [Display(Name = "Město")]
	public string City { get; set; }
	
    [Required]
    [Display(Name = "Číslo popisné")]
	public string BuildingNumber { get; set; }
	
    [Display(Name = "Číslo bytu")]
	public int?  ApartamentNumber { get; set; }
	
    [Required]
    [Display(Name = "PSČ")]
	public string PostalCode { get; set; }
	
	// Studijni programy
    [Required]
    [Display(Name = "Primární studijní program")]
	public int PrimaryProgramId { get; set; }
	
    [Display(Name = "Druhý studijí program")]
	public int StudyProgramId { get; set; }	
	
    [Display(Name = "Třetí studijní program")]
	public int TertiaryProgramId { get; set; }
}