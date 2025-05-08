using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using UserRegistrationWizard.Server.Interfaces;
using UserRegistrationWizard.Server.Models;
using UserRegistrationWizard.Server.Models.Dto;

namespace UserRegistrationWizard.Server.Services
{
    public class CountryService : ICountryService
    {
        private readonly UserRegistrationWizardDbContext _dbContext;
        private readonly IMapper _mapper;

        public CountryService(UserRegistrationWizardDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CountryDto>> GetCountries(CancellationToken cancellationToken)
        {
            return await _dbContext.Countries
                .ProjectTo<CountryDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
