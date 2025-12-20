using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entities.Identity;

namespace Talabat.Infrastructure._Identity
{
    public static class ApplicationIdentityContextSeed
    {
        public static async Task SeedUserAsync(UserManager<ApplicationUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new ApplicationUser()
                {
                    DisplayName = "Moaz Ahmed",
                    Email = "moaz.ahmd@gmail.com",
                    UserName = "moaz.ahmed",
                    PhoneNumber = "01112233445"
                }; 

                await userManager.CreateAsync(user, "P@ssw0rd");
            }
        }
    }
}
