using Microsoft.EntityFrameworkCore;
using Studentportal.Models.Entities;

namespace Studentportal.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        }
        public DbSet<Student>students { get; set; }
    }
}
