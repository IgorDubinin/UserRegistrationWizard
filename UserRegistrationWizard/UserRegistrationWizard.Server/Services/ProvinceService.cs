using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using UserRegistrationWizard.Server.Interfaces;
using UserRegistrationWizard.Server.Models;
using UserRegistrationWizard.Server.Models.Dto;

namespace UserRegistrationWizard.Server.Services
{
    public class ProvinceService : IProvinceService
    {
        private readonly UserRegistrationWizardDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProvinceService(UserRegistrationWizardDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProvinceDto>> GetProvincesByCountryId(long countryId, CancellationToken cancellationToken)
        {
            return await _dbContext.Provinces
                .Where(x => x.CountryId == countryId)
                .ProjectTo<ProvinceDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
