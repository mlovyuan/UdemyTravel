﻿using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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

            // this method will add Location property in the response headers and we can use the value to consume GetTouristRoutesById endpoint directly
            return CreatedAtRoute("GetTouristRoutesById", new { touristRouteId = touristRouteDto.Id }, touristRouteDto);
        }

        [HttpPut("{touristRouteId:Guid}")]
        public IActionResult UpdateTouristRoute([FromRoute] Guid touristRouteId, [FromBody] TouristRouteForUpdateDto touristRouteForUpdateDto)
        {
            if (!_touristRouteRepository.TouristRouteExists(touristRouteId))
            {
                return NotFound($"旅遊路線{touristRouteId}不存在");
            }

            var touristRouteFromRepo = _touristRouteRepository.GetTouristRoute(touristRouteId);
            _mapper.Map(touristRouteForUpdateDto, touristRouteFromRepo);
            // above line ady update repo model from Dto since EF and context, so only need to save below directly
            _touristRouteRepository.Save();
            return NoContent();
        }

        [HttpPut("{touristRouteId:Guid}")]
        public IActionResult PartiallyUpdateTouristRoute([FromRoute] Guid touristRouteId,
            [FromBody] JsonPatchDocument<TouristRouteForUpdateDto> patchDocument)
        {
            if (!_touristRouteRepository.TouristRouteExists(touristRouteId))
            {
                return NotFound($"旅遊路線{touristRouteId}不存在");
            }
            var touristRouteFromRepo = _touristRouteRepository.GetTouristRoute(touristRouteId);
            var touristRouteToPatch = _mapper.Map<TouristRouteForUpdateDto>(touristRouteFromRepo);
            patchDocument.ApplyTo(touristRouteToPatch, ModelState);
            if (!TryValidateModel(touristRouteToPatch))
            {
                return ValidationProblem(ModelState);
            }


            _mapper.Map(touristRouteToPatch, touristRouteFromRepo);
            _touristRouteRepository.Save();
            return NoContent();
        }
    }
}