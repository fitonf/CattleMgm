using Microsoft.AspNetCore.Identity;

namespace CattleMgm.Models
{
    public static class UserAndRoleDataInitializer
    {
        public static void SeedData(UserManager<ApplicationUser> userManager)
        {
            SeedUsers(userManager);
        }

        private static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByEmailAsync("fiton.frangu@dataprognet-ks.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "fiton.frangu@dataprognet-ks.com";
                user.Email = "fiton.frangu@dataprognet-ks.com";
                user.FirstName = "Fiton";
                user.LastName = "Frangu";

                IdentityResult result = userManager.CreateAsync(user, "123456789Aa#").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }
        }
    }
}
