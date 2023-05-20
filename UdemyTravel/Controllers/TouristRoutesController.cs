using Microsoft.AspNetCore.Mvc;
using UdemyTravel.Services;

namespace UdemyTravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TouristRoutesController : ControllerBase
    {
        private ITouristRouteRepository _touristRouteRepository;

        public TouristRoutesController(ITouristRouteRepository touristRouteRepository)
        {
            _touristRouteRepository = touristRouteRepository;
        }

        [HttpGet]
        public IActionResult GetAllTouristRotes()
        {
            return Ok(_touristRouteRepository.GetAllTouristRoute());
        }
    }
}
