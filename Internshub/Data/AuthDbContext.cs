using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Internshub.Data
{
    public class AuthDbContext : IdentityDbContext<IdentityUser>
    {
      public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
      {
        
      }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var adminRoleId = "d296edd3-1120-4e68-a8f6-8820333c64be";
            var userRoleId = "b3983129-98fc-480a-b913-a2efda168cf5";

            var roles = new List<IdentityRole>
            {
              new IdentityRole
              {
               Name= "Admin",
               NormalizedName= "ADMIN",
               Id= adminRoleId,
               ConcurrencyStamp = adminRoleId

              },
              new IdentityRole
              {
                  Name = "User",
                  NormalizedName="USER",
                  Id= userRoleId,
                  ConcurrencyStamp = userRoleId
              }
              


            };

            builder.Entity<IdentityRole>().HasData(roles);

            var adminId = "4a930840-ebf6-4d4f-adf1-87aa7965d4a8";


            var adminUser = new IdentityUser
            {

                UserName = "inayat@gmail.com",
                Email = "inayat@gmail.com",
                NormalizedUserName = "inayat@gmail.com".ToUpper(),
                NormalizedEmail = "inayat@gmail.com".ToUpper(),
                 Id= adminId,           
             
            };

            adminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(adminUser, "AdminUser11@");
             
            builder.Entity<IdentityUser>().HasData(adminUser);

            var adminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId= userRoleId,
                    UserId= adminId
                },

                new IdentityUserRole<string>
                { 
                    RoleId= adminRoleId,
                    UserId= adminId
                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }


    }
}
