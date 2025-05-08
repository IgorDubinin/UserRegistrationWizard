using Microsoft.AspNetCore.Mvc;
using UserRegistrationWizard.Server.Interfaces;
using UserRegistrationWizard.Server.Models.Dto;

namespace UserRegistrationWizard.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public async Task<IEnumerable<CountryDto>> Get(CancellationToken cancellationToken)
        {
            return await _countryService.GetCountries(cancellationToken);
        }
    }
}
