using UserRegistrationWizard.Server.Models.Dto;

namespace UserRegistrationWizard.Server.Interfaces
{
    public interface ICountryService
    {
        public Task<IEnumerable<CountryDto>> GetCountries(CancellationToken cancellationToken);
    }
}
