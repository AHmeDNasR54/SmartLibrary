
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartLibrary.Models;

namespace SmartLibrary.DataAccess.Seeder
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> _userManager)
        {
            var usersCount = await _userManager.Users.CountAsync();
            if (usersCount <= 0)
            {
                var adminUser = new ApplicationUser()
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    PhoneNumber = "01143982960",
                    EmailConfirmed = true,
                };
                await _userManager.CreateAsync(adminUser, "P@ssw0rd123Pass");
                await _userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
