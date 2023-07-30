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

        public IEnumerable<TouristRoute> GetAllTouristRoute(string keyword, string ratingOperator, int ratingValue)
        {
            IQueryable<TouristRoute> result = _context.TouristRoutes.Include(x => x.TouristRoutePictures);
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();
                result.Where(x => x.Title.Contains(keyword));
            }
            if (ratingValue >= 0)
            {
                result = ratingOperator switch
                {
                    "largerThan" => result.Where(x => x.Rating >= ratingValue),
                    "lessThan" => result.Where(x => x.Rating <= ratingValue),
                    _ => result.Where(x => x.Rating == ratingValue),
                };
            }
            return result.ToList();
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
