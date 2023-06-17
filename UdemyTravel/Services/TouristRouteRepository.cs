using Microsoft.EntityFrameworkCore;
using UdemyTravel.Database;
using UdemyTravel.Models;

namespace UdemyTravel.Services
{
    public class TouristRouteRepository : ITouristRouteRepository
    {
        private readonly AppDbContext _context;

        public TouristRouteRepository(AppDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<TouristRoute> GetAllTouristRoute()
        {
            return _context.TouristRoutes.Include(x => x.TouristRoutePictures);
        }

        public TouristRoute GetTouristRoute(Guid touristRouteId)
        {
            return _context.TouristRoutes.Include(x => x.TouristRoutePictures).FirstOrDefault(x => x.Id == touristRouteId);
        }

        public bool TouristRouteExists(Guid touristRouteId)
        {
            return _context.TouristRoutes.Any(x => x.Id == touristRouteId);
        }

        public IEnumerable<TouristRoutePicture> GetPicturesByTouristRouteId(Guid touristRouteId)
        {
            return _context.TouristRoutePictures.Where(x => x.TouristRouteId == touristRouteId).ToList();
        }

        public TouristRoutePicture GetPicture(int pictureId)
        {
            return _context.TouristRoutePictures.Where(x => x.Id == pictureId).FirstOrDefault();
        }
    }
}
