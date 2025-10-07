using System;
using System.ComponentModel.DataAnnotations;

namespace PP2App.Models
{
    public class BinaryModel
    {
        [Required(ErrorMessage = "Debe ingresar el valor de a.")]
        [RegularExpression("^[01]+$", ErrorMessage = "Solo se permiten 0 y 1.")]
        [StringLength(8, MinimumLength = 2, ErrorMessage = "La longitud debe ser entre 2 y 8.")]
        [CustomValidation(typeof(BinaryModel), nameof(ValidateLengthMultipleOfTwo))]
        public string? A { get; set; }

        [Required(ErrorMessage = "Debe ingresar el valor de b.")]
        [RegularExpression("^[01]+$", ErrorMessage = "Solo se permiten 0 y 1.")]
        [StringLength(8, MinimumLength = 2, ErrorMessage = "La longitud debe ser entre 2 y 8.")]
        [CustomValidation(typeof(BinaryModel), nameof(ValidateLengthMultipleOfTwo))]
        public string? B { get; set; }

        public static ValidationResult? ValidateLengthMultipleOfTwo(string? value, ValidationContext context)
        {
            if (string.IsNullOrEmpty(value)) return ValidationResult.Success;
            return value.Length % 2 == 0
                ? ValidationResult.Success
                : new ValidationResult("La longitud debe ser múltiplo de 2.");
        }
    }
}
