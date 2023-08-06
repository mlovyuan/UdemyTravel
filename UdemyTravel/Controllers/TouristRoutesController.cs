using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;
using UdemyTravel.DTOs;
using UdemyTravel.Models;
using UdemyTravel.ResourceParameters;
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
        public IActionResult GetAllTouristRotes([FromQuery] TouristRouteResourceParameters touristRouteResourceParameters)
        {
            var touristRoutes = _touristRouteRepository.GetAllTouristRoute(touristRouteResourceParameters.Keyword,
                touristRouteResourceParameters.RatingOperator,
                touristRouteResourceParameters.RatingValue);

            if (touristRoutes.IsNullOrEmpty())
            {
                return NotFound("沒有旅遊路線");
            }
            var touristRoutesDto = _mapper.Map<IEnumerable<TouristRouteDto>>(touristRoutes);
            return Ok(touristRoutesDto);
        }

        [HttpGet("{touristRouteId:Guid}", Name = "GetTouristRoutesById")]
        public IActionResult GetTouristRoutesById(Guid touristRouteId)
        {
            var touristRoute = _touristRouteRepository.GetTouristRoute(touristRouteId);

            if (touristRoute is null)
            {
                return NotFound($"旅遊路線{touristRouteId}不存在");
            }

            var touristRouteDto = _mapper.Map<TouristRouteDto>(touristRoute);
            return Ok(touristRouteDto);
        }

        [HttpPost]
        public IActionResult CreateTouristRoute([FromBody] TouristRouteForCreationDto touristRouteForCreationDto)
        {
            var touristRouteModel = _mapper.Map<TouristRoute>(touristRouteForCreationDto);
            _touristRouteRepository.AddTouristRoute(touristRouteModel);
            _touristRouteRepository.Save();
            var touristRouteDto = _mapper.Map<TouristRouteDto>(touristRouteModel);
            return CreatedAtRoute("GetTouristRoutesById", new { touristRouteId = touristRouteDto.Id }, touristRouteDto);
        }
    }
}