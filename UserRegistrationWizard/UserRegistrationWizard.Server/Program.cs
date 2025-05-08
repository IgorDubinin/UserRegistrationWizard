using Microsoft.EntityFrameworkCore;
using UserRegistrationWizard.Server.Initializer;
using UserRegistrationWizard.Server.Interfaces;
using UserRegistrationWizard.Server.Models;
using UserRegistrationWizard.Server.Profiles;
using UserRegistrationWizard.Server.Services;

namespace UserRegistrationWizard.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularDev", policy =>
                {
                    policy.WithOrigins("http://localhost:56027")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            builder.Services.AddDbContext<UserRegistrationWizardDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddHostedService<DbInitializer>();
            builder.Services.AddScoped<ICountryService, CountryService>();
            builder.Services.AddScoped<IProvinceService, ProvinceService>();
            builder.Services.AddScoped<IUserService, UserService>();

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("AllowAngularDev");

            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
