using Microsoft.AspNetCore.Mvc;
using UserRegistrationWizard.Server.Interfaces;
using UserRegistrationWizard.Server.Models.Dto;

namespace UserRegistrationWizard.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProvinceController : ControllerBase
    {
        private readonly IProvinceService _provinceService;

        public ProvinceController(IProvinceService provinceService)
        {
            _provinceService = provinceService;
        }

        [HttpGet]
        public async Task<IEnumerable<ProvinceDto>> GetProvincesByCountryId(long countryId, CancellationToken cancellationToken)
        {
            return await _provinceService.GetProvincesByCountryId(countryId, cancellationToken);
        }
    }
}
