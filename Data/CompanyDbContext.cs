using Microsoft.EntityFrameworkCore;
using CompanyManagementApp.Models;

namespace CompanyManagementApp.Data
{
    public class CompanyDbContext : DbContext
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options)
            : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
    }
}