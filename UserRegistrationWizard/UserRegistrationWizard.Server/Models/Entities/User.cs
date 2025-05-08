namespace UserRegistrationWizard.Server.Models.Entities
{
    public class User
    {
        public long Id { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public bool Agreed { get; set; }

        public long ProvinceId { get; set; }

        public Province Province { get; set; }
    }
}
