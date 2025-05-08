using UserRegistrationWizard.Server.Models.Dto;

namespace UserRegistrationWizard.Server.Interfaces
{
    public interface IUserService
    {
        public Task<bool> UserExistsByEmailAsync(string email, CancellationToken cancellationToken);
        public Task RegisterUserAsync(RegisterDto registerDto, CancellationToken cancellationToken);
    }
}
