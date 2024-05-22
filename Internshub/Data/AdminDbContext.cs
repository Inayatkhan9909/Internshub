using Internshub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Internshub.Data
{
    public class AdminDbContext : DbContext
    {
        
        public AdminDbContext(DbContextOptions<AdminDbContext> options)
            :base(options)
        {

        }
        public DbSet<EnrollModel> Interns { get; set; }
    }
}
