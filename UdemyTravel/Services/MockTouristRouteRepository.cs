using UdemyTravel.Models;

namespace UdemyTravel.Services
{
    public class MockTouristRouteRepository : ITouristRouteRepository
    {
        private List<TouristRoute> _touristRoutes;
        public MockTouristRouteRepository()
        {
            if (this._touristRoutes == null)
            {
                InitializeTOuristRoute();
            }
        }

        private void InitializeTOuristRoute()
        {
            _touristRoutes = new List<TouristRoute>
            {
                new TouristRoute {
                    Id = Guid.NewGuid(),
                    Title = "日月潭",
                    Description="日月潭真好玩",
                    OriginalPrice = 1299,
                    Features = "<p>吃住行</p>",
                    Fees = "<p>交通費用自理</p>",
                    Notes="<p>注意安全</p>"
                },
                new TouristRoute {
                    Id = Guid.NewGuid(),
                    Title = "九份",
                    Description="九份真好玩",
                    OriginalPrice = 1299,
                    Features = "<p>吃住行</p>",
                    Fees = "<p>交通費用自理</p>",
                    Notes="<p>注意安全</p>"
                }
            };
        }

        public IEnumerable<TouristRoute> GetAllTouristRoute()
        {
            return _touristRoutes;
        }

        public TouristRoute GetTouristRoute(Guid touristRouteId)
        {
            return _touristRoutes.FirstOrDefault(x => x.Id == touristRouteId);
        }
    }
}
