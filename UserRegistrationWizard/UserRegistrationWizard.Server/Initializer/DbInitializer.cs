using Microsoft.EntityFrameworkCore;
using UserRegistrationWizard.Server.Models;

namespace UserRegistrationWizard.Server.Initializer
{
    public class DbInitializer : IHostedService
    {
        private readonly IServiceProvider _provider;

        public DbInitializer(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _provider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<UserRegistrationWizardDbContext>();
            await db.Database.MigrateAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
