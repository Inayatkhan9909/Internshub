using Internshub.Models;
using Microsoft.EntityFrameworkCore;

namespace Internshub.Data
{
    public class RegistrationDbContext : DbContext
    {
        public RegistrationDbContext(DbContextOptions<RegistrationDbContext> options)
          : base(options)
        {
        }

        public DbSet<EnrollModel> Interns { get; set; }
    }
}
