using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    public class ModuleContext : DbContext
    {
        public ModuleContext(DbContextOptions<ModuleContext> options) : base(options)
        {
        }

        public DbSet<Module> modules { get; set; }
    }
}