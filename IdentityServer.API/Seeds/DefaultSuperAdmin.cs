using IdentityModel;
using IdentityServer.API.Enums;
using IdentityServer.API.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.API.Seeds
{
    public static class DefaultSuperAdmin
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            var defaultUser = new ApplicationUser
            {
                UserName = "ahmedayman",
                Email = "ahmedayman6038@gmail.com",
                FirstName = "Ahmed",
                LastName = "Ayman",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Ahmed@123");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Moderator.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.SuperAdmin.ToString());
                    await userManager.AddClaimsAsync(defaultUser, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Ahmed Ayman"),
                            new Claim(JwtClaimTypes.GivenName, "Ahmed"),
                            new Claim(JwtClaimTypes.FamilyName, "Ayman"),
                            new Claim(JwtClaimTypes.WebSite, "https://www.linkedin.com/in/ahmedayman6038/")
                        });
                }

            }
        }
    }
}
