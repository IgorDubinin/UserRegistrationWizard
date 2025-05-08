using Microsoft.EntityFrameworkCore;
using UserRegistrationWizard.Server.Models.Entities;

namespace UserRegistrationWizard.Server.Models
{
    public class UserRegistrationWizardDbContext : DbContext
    {
        public UserRegistrationWizardDbContext(DbContextOptions<UserRegistrationWizardDbContext> options)
            : base(options) { }

        public DbSet<Country> Countries => Set<Country>();
        public DbSet<Province> Provinces => Set<Province>();

        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired();
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired();

                entity.HasOne<Country>()
                    .WithMany()
                    .HasForeignKey(p => p.CountryId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Email).IsRequired();
                entity.Property(u => u.PasswordHash).IsRequired();

                entity.HasOne(u => u.Province)
                    .WithMany()
                    .HasForeignKey(u => u.ProvinceId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
