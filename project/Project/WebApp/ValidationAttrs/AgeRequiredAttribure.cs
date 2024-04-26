using System.ComponentModel.DataAnnotations;

namespace WebApp.ValidationAttrs;

public class AgeRequiredAttribure : ValidationAttribute {
	private readonly int _minimumAge;

	public AgeRequiredAttribure(int minimumAge) {
		_minimumAge = minimumAge;
	}

	protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
		if (value is DateTime birthDate) {
			DateTime today = DateTime.Today;
			int age = today.Year - birthDate.Year;

			if (birthDate.Date > today.AddYears(-age))
				age--;

			if (age >= _minimumAge) {
				return ValidationResult.Success;
			}
			return new ValidationResult($"Minimální věk atributu \"{validationContext.DisplayName}\" je {_minimumAge} let");
		}

		return new ValidationResult($"{validationContext.DisplayName} musí být datum");
	}
}