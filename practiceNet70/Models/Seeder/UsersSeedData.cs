using System;
using System.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using practiceNet70.Data;

namespace practiceNet70.Models.Seeder
{
	public class UsersSeedData
	{
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw= "Secret123@")
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                string[] roles = new string[] { "Administrator", "Buyer" };

                var adminID = await EnsureUser(serviceProvider, testUserPw, "Administrator@example.com");
                await EnsureRole(serviceProvider, adminID, "Administrator");
               
                var managerID = await EnsureUser(serviceProvider, testUserPw, "Buyer@example.com");
                await EnsureRole(serviceProvider, managerID, "Buyer");
            }
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider, string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<User>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new User
                {
                    UserName = UserName,
                    Name = UserName,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(user, testUserPw);
            }

            if (user == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider, string uid, string role)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            IdentityResult IR;
            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<User>>();

            if (userManager == null)
            {
                throw new Exception("userManager is null");
            }

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }
    }
}

