using Microsoft.EntityFrameworkCore;

namespace api.Models
{
        public class CourseContext : DbContext
    {
        public CourseContext(DbContextOptions<CourseContext> options)
        : base(options)
    {
    }

        public DbSet<Course> courses { get; set; }
    }
}