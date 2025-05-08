namespace UserRegistrationWizard.Server.Models.Dto
{
    public class RegisterDto
    {
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string ConfirmPassword { get; set; } = null!;

        public bool Agreed { get; set; }

        public long ProvinceId { get; set; }
    }
}
