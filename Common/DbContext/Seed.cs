using DTO.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Common.DataContext
{
    public static class Seed
    {
        public static async Task SeedDefaultAuthUser(IServiceProvider serviceProvider)
        {
            var _services = serviceProvider.CreateScope();
            var userManager = _services.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = _services.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var role = await roleManager.FindByNameAsync("SibinAdmin");
            if (role == null)
            {
                await roleManager.CreateAsync(new IdentityRole
                {
                    Name = "SibinAdmin"
                });
            }
            var user = await userManager.FindByNameAsync("SibinAdmin");
            if (user == null)
            {
                user = new ApplicationUser
                {
                    FirstName = "Sibin",
                    LastName = "Admin",
                    UserName = "SibinAdmin",
                    Email = "admin@sibingroup.com",
                    PhoneNumber = "7797000328"
                };
                var result = await userManager.CreateAsync(user, "Sibin@123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "SibinAdmin");
                }
            }


        }
    }
}
