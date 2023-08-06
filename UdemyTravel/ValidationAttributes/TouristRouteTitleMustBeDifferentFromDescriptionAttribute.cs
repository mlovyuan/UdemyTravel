using System.ComponentModel.DataAnnotations;
using UdemyTravel.DTOs;

namespace UdemyTravel.ValidationAttributes
{
    public class TouristRouteTitleMustBeDifferentFromDescriptionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var touristRouteForCreationDto = (TouristRouteForCreationDto)validationContext.ObjectInstance;
            if (touristRouteForCreationDto.Title == touristRouteForCreationDto.Description)
            {
                return new ValidationResult(
                    "Title and Description should be different.",
                    new[] { "TouristRouteForCreationDto" }
                );
            }
            return ValidationResult.Success;
        }
    }
}
