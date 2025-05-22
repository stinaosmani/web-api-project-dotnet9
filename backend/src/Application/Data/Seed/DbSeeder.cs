using Infrastructure.Persistence;
using backend.src.Application.Helpers;

namespace backend.src.Application.Data.Seed;

public static class DbSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var userRepo = scope.ServiceProvider.GetRequiredService<IRepository<User, Guid>>();

        if (!userRepo.AsQueryable().ToList().Any())
        {
            var admin = new User
            {
                Id = Guid.NewGuid(),
                Username = "admin",
                Password = PasswordHasher.Hash("admin123!"),
                FirstName = "Admin",
                LastName = "User",
                DateOfBirth = new DateTime(1990, 1, 1),
                IsDeleted = 0
            };

            await userRepo.AddAsync(admin);
            Console.WriteLine("Admin user seeded: username=admin, password=admin123!");
        }
    }
}
