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
            return _context.TouristRoutes;
        }

        public TouristRoute GetTouristRoute(Guid touristRouteId)
        {
            return _context.TouristRoutes.FirstOrDefault(x => x.Id == touristRouteId);
        }
    }
}
