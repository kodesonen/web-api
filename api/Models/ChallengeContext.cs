using Microsoft.EntityFrameworkCore;

namespace api.Models
{
        public class ChallengeContext : DbContext
    {
        public ChallengeContext(DbContextOptions<ChallengeContext> options)
        : base(options)
    {
    }

        public DbSet<Challenge> challenges { get; set; }
    }
}