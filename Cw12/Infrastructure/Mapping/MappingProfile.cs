using System.Linq;
using AutoMapper;
using Cw12.Data.Models;
using Cw12.Services.Dtos;

namespace Cw12.Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Trip, TripDto>()
                .ForMember(d => d.Countries,
                    o => o.MapFrom(s => s.CountryTrips.Select(ct => ct.Country)))
                .ForMember(d => d.Clients,
                    o => o.MapFrom(s => s.ClientTrips.Select(ct => ct.Client)));

            CreateMap<Country, CountryDto>();
            CreateMap<Client, ClientSimpleDto>();
        }
    }
}