using AutoMapper;
using UdemyTravel.DTOs;
using UdemyTravel.Models;

namespace UdemyTravel.Profiles
{
    public class TouristRouteProfile : Profile
    {
        public TouristRouteProfile()
        {
            // first line will map filed name automatically, the other add the rule to projection
            CreateMap<TouristRoute, TouristRouteDto>()
                .ForMember(
                    destinationMember => destinationMember.Price,
                    opt => opt.MapFrom(sourceMember => sourceMember.OriginalPrice * (decimal)(sourceMember.DiscountRate ?? 1))
                ).ForMember(
                    destinationMember => destinationMember.TravelDays,
                    opt => opt.MapFrom(sourceMember => sourceMember.TravelDays.ToString())
                ).ForMember(
                    destinationMember => destinationMember.TripType,
                    opt => opt.MapFrom(sourceMember => sourceMember.TripType.ToString())
                ).ForMember(
                    destinationMember => destinationMember.DepartureCity,
                    opt => opt.MapFrom(sourceMember => sourceMember.DepartureCity.ToString())
                );

            CreateMap<TouristRouteForCreationDto, TouristRoute>()
                .ForMember(
                    destinationMember => destinationMember.Id,
                    opt => opt.MapFrom(sourceMember => Guid.NewGuid())
                );

            CreateMap<TouristRouteForUpdateDto, TouristRoute>();
            CreateMap<TouristRoute, TouristRouteForUpdateDto>();
        }
    }
}
