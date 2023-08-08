using System.ComponentModel.DataAnnotations;

namespace UdemyTravel.DTOs
{
    public class TouristRouteForUpdateDto : TouristRouteForManipulationDto
    {
        [Required(ErrorMessage = "更新必備"), MaxLength(1500)]
        public override string Description { get; set; }
    }
}
