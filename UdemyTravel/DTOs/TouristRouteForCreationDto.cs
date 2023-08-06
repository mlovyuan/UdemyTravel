using System.ComponentModel.DataAnnotations;
using UdemyTravel.ValidationAttributes;

namespace UdemyTravel.DTOs
{
    [TouristRouteTitleMustBeDifferentFromDescriptionAttribute]
    public class TouristRouteForCreationDto// : IValidatableObject
    {
        [Required(ErrorMessage = "title 不可為空"), MaxLength(100)]
        public required string Title { get; set; }
        [Required, MaxLength(1500)]
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime? DepartureTime { get; set; }
        public string? Features { get; set; }
        public string? Fees { get; set; }
        public string? Notes { get; set; }
        public double? Rating { get; set; }
        public string? TravelDays { get; set; }
        public string? TripType { get; set; }
        public string? DepartureCity { get; set; }
        public ICollection<TouristRoutePictureForCreationDto> TouristRoutePictures { get; set; }
        = new List<TouristRoutePictureForCreationDto>();

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (Title == Description)
        //    {
        //        yield return new ValidationResult(
        //            "Title and Description should be different.",
        //            new[] { "TouristRouteForCreationDto" }
        //        );
        //    }
        //}
    }
}
