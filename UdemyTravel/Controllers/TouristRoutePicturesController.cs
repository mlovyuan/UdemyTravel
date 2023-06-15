using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using UdemyTravel.DTOs;
using UdemyTravel.Services;

namespace UdemyTravel.Controllers
{
    [Route("api/touristRoutes/{touristRouteId}/pictures")]
    [ApiController]
    public class TouristRoutePicturesController : ControllerBase
    {
        private ITouristRouteRepository _touristRouteRepository;
        private IMapper _mapper;

        public TouristRoutePicturesController(ITouristRouteRepository touristRouteRepository, IMapper mapper)
        {
            _touristRouteRepository = touristRouteRepository ?? throw new ArgumentNullException(nameof(touristRouteRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public IActionResult GetPictureListForTouristRoute(Guid touristRouteId)
        {
            if (!_touristRouteRepository.TouristRouteExists(touristRouteId))
            {
                return NotFound($"旅遊路線{touristRouteId}不存在");
            }

            var touristRoutePictures = _touristRouteRepository.GetPicturesByTouristRouteId(touristRouteId);
            if (touristRoutePictures.IsNullOrEmpty())
            {
                return NotFound("照片不存在");
            }

            return Ok(_mapper.Map<IEnumerable<TouristRoutePictureDto>>(touristRoutePictures));
        }

        [HttpGet("{pictureId}")]
        public IActionResult GetPicture(Guid touristRouteId, int pictureId)
        {
            // check parent table first, due to table's foreign key
            if (!_touristRouteRepository.TouristRouteExists(touristRouteId))
            {
                return NotFound($"旅遊路線{touristRouteId}不存在");
            }
            var picture = _touristRouteRepository.GetPicture(pictureId);
            if (picture is null)
            {
                return NotFound("照片不存在");
            }
            return Ok(_mapper.Map<TouristRoutePictureDto>(picture));
        }
    }
}
