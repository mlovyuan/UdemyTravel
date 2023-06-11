using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
            var touristRoutes = _touristRouteRepository.GetAllTouristRoute();
            return touristRoutes.IsNullOrEmpty() ? NotFound("沒有旅遊路線") : Ok(touristRoutes);
        }

        [HttpGet("{touristRouteId:Guid}")]
        public IActionResult GetTouristRotesById(Guid touristRouteId)
        {
            var touristRoute = _touristRouteRepository.GetTouristRoute(touristRouteId);
            return touristRoute is null ? NotFound($"旅遊路線{touristRouteId}不存在") : Ok(touristRoute);
        }
    }
}
