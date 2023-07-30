using UdemyTravel.Models;

namespace UdemyTravel.Services
{
    public interface ITouristRouteRepository
    {
        IEnumerable<TouristRoute> GetAllTouristRoute(string keyword, string ratingOperator, int ratingValue);
        TouristRoute GetTouristRoute(Guid touristRouteId);
        bool TouristRouteExists(Guid touristRouteId);
        IEnumerable<TouristRoutePicture> GetPicturesByTouristRouteId(Guid touristRouteId);
        TouristRoutePicture GetPicture(int pictureId);
    }
}
