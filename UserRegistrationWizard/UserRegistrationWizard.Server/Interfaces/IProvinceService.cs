using UserRegistrationWizard.Server.Models.Dto;

namespace UserRegistrationWizard.Server.Interfaces
{
    public interface IProvinceService
    {
        public Task<IEnumerable<ProvinceDto>> GetProvincesByCountryId(long countryId, CancellationToken cancellationToken);
    }
}
