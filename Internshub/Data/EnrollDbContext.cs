using Internshub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Internshub.Data
{
    public class EnrollDbContext : DbContext
    {
        public EnrollDbContext(DbContextOptions<EnrollDbContext> options)
   : base(options)
        {

        }
        public DbSet<EnrollModel> Enrolls { get; set; }
    }
}
