using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserRegistrationWizard.Server.Interfaces;
using UserRegistrationWizard.Server.Models;
using UserRegistrationWizard.Server.Models.Dto;
using UserRegistrationWizard.Server.Models.Entities;

namespace UserRegistrationWizard.Server.Services
{
    public class UserService : IUserService
    {
        private readonly UserRegistrationWizardDbContext _dbContext;
        private readonly PasswordHasher<string> _passwordHasher = new();

        public UserService(UserRegistrationWizardDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> UserExistsByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _dbContext.Users.AnyAsync(u => u.Email == email, cancellationToken);
        }

        public async Task RegisterUserAsync(RegisterDto registerDto, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Email = registerDto.Email,
                PasswordHash = _passwordHasher.HashPassword(null, registerDto.Password),
                Agreed = registerDto.Agreed,
                ProvinceId = registerDto.ProvinceId,
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
