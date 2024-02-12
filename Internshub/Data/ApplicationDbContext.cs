using Internshub.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Internshub.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var superadminRoleId = "a7d1fda4-e6b8-4915-8a88-1ca0d42237e0";
            var adminRoleId = "d296edd3-1120-4e68-a8f6-8820333c64be";
            var userRoleId = "b3983129-98fc-480a-b913-a2efda168cf5";

            var roles = new List<IdentityRole>
            {
              new IdentityRole
              {
               Name= "SuperAdmin",
               NormalizedName= "SUPERADMIN",
               Id= superadminRoleId,
               ConcurrencyStamp = superadminRoleId

              },
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

            var superadminId = "537154a4-ecbb-41de-b4b8-10af0f9d1836";

            var superadminUser = new ApplicationUser
            {
                FirstName = "Inayat ULlah",
                LastName = "Khan",
                UserName = "inayat99",
                Email = "inayat@gmail.com",
                NormalizedUserName = "inayat99".ToUpper(),
                NormalizedEmail = "inayat@gmail.com".ToUpper(),
                Id = superadminId,

            };

            superadminUser.PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(superadminUser, "Inayat@12");

            builder.Entity<ApplicationUser>().HasData(superadminUser);


            var adminId = "4a930840-ebf6-4d4f-adf1-87aa7965d4a8";


            var adminUser = new ApplicationUser
            {

                UserName = "owais89",
                Email = "owais@gmail.com",
                NormalizedUserName = "owais89".ToUpper(),
                NormalizedEmail = "owais@gmail.com".ToUpper(),
                Id = adminId,

            };

            adminUser.PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(adminUser, "Owais@12");

            builder.Entity<ApplicationUser>().HasData(adminUser);

            var superadminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId= userRoleId,
                    UserId= superadminId
                },

                new IdentityUserRole<string>
                {
                    RoleId= adminRoleId,
                    UserId= superadminId
                },
                new IdentityUserRole<string>
                {
                    RoleId= superadminRoleId,
                    UserId= superadminId
                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(superadminRoles);

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
