using Internshub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Internshub.Data
{
    public class AddInternshipDbContext : DbContext    {

       
                    public AddInternshipDbContext(DbContextOptions<AddInternshipDbContext> options)
          : base(options)
        {

        }

        public DbSet<AddInternshipModel> Internship { get; set; }
    }
}
