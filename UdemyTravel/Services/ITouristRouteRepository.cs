using UdemyTravel.Models;

namespace UdemyTravel.Services
{
    public interface ITouristRouteRepository
    {
        IEnumerable<TouristRoute> GetAllTouristRoute();
        TouristRoute GetTouristRoute(Guid touristRouteId);
        bool TouristRouteExists(Guid touristRouteId);
        IEnumerable<TouristRoutePicture> GetPicturesByTouristRouteId(Guid touristRouteId);
    }
}
