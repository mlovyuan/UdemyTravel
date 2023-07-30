using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;
using UdemyTravel.DTOs;
using UdemyTravel.Models;
using UdemyTravel.Services;

namespace UdemyTravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TouristRoutesController : ControllerBase
    {
        private ITouristRouteRepository _touristRouteRepository;
        private IMapper _mapper;

        public TouristRoutesController(ITouristRouteRepository touristRouteRepository, IMapper mapper)
        {
            _touristRouteRepository = touristRouteRepository;
            _mapper = mapper;
        }

        [HttpGet, HttpHead]
        public IActionResult GetAllTouristRotes([FromQuery] string keyword, string rating)
        {
            Regex regex = new Regex(@"([A-Za-z0-9\-]+)(\d+)");
            string operatorType = "";
            int ratingValue = -1;
            Match match = regex.Match(rating);
            if (match.Success)
            {
                operatorType = match.Groups[1].Value;
                ratingValue = Int32.Parse(match.Groups[2].Value);
            }

            var touristRoutes = _touristRouteRepository.GetAllTouristRoute(keyword, operatorType, ratingValue);

            if (touristRoutes.IsNullOrEmpty())
            {
                return NotFound("沒有旅遊路線");
            }
            var touristRoutesDto = _mapper.Map<IEnumerable<TouristRouteDto>>(touristRoutes);
            return Ok(touristRoutesDto);
        }

        [HttpGet("{touristRouteId:Guid}"), HttpHead]
        public IActionResult GetTouristRotesById(Guid touristRouteId)
        {
            var touristRoute = _touristRouteRepository.GetTouristRoute(touristRouteId);

            if (touristRoute is null)
            {
                return NotFound($"旅遊路線{touristRouteId}不存在");
            }

            var touristRouteDto = _mapper.Map<TouristRouteDto>(touristRoute);
            return Ok(touristRouteDto);
        }
    }
}
