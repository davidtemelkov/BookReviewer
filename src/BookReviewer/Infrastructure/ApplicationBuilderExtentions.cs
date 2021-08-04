namespace BookReviewer.Infrastructure
{
    using BookReviewer.Data;
    using BookReviewer.Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using static BookReviewer.Areas.Admin.AdminConstants;

    public static class ApplicationBuilderExtentions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            SeedGenres(services);
            SeedAdministrator(services);
            MigrateDatabase(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<ApplicationDbContext>();

            data.Database.Migrate();
        }

        public static void SeedGenres(IServiceProvider services)
        {
            var data = services.GetRequiredService<ApplicationDbContext>();

            if (data.Genres.Any())
            {
                return;
            }

            data.Genres.AddRange(new[]
            {
            new Genre { Name = "Romance"},
            new Genre { Name = "Mystery"},
            new Genre { Name = "Fantasy"},
            new Genre { Name = "Fiction"},
            new Genre { Name = "Thriller"},
            new Genre { Name = "Horror"},
            new Genre { Name = "Young adult"},
            new Genre { Name = "Adult"},
            new Genre { Name = "Children"},
            new Genre { Name = "Self-help"},
            new Genre { Name = "Religious"},
            new Genre { Name = "Autobiography"},
            new Genre { Name = "Historical"},
            new Genre { Name = "Non-fiction"},
            new Genre { Name = "Classics"},
            new Genre { Name = "Politics"}
        });

            data.SaveChanges();
        }

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = AdministratorRoleName };

                    await roleManager.CreateAsync(role);

                    const string username = "admin";
                    const string adminEmail = "admin@crs.com";
                    const string adminPassword = "admin12";

                    var user = new User
                    {
                        Email = adminEmail,
                        UserName = username
                    };

                    await userManager.CreateAsync(user, adminPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}
