using Andal.Models;
using Microsoft.EntityFrameworkCore;

namespace Andal.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<JobTitle> JobTitles { get; set; }
        public DbSet<JobPosition> JobPositions { get; set; }
    }
}
