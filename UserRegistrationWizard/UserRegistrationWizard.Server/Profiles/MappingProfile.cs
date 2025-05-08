using AutoMapper;
using UserRegistrationWizard.Server.Models.Dto;
using UserRegistrationWizard.Server.Models.Entities;

namespace UserRegistrationWizard.Server.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Country, CountryDto>();
            CreateMap<Province, ProvinceDto>();
        }
    }
}
