using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using api.Models;

namespace api
{
        public class DataContext : IdentityDbContext<User>
        {
            public DataContext(DbContextOptions<DataContext> options)
            : base(options)
            {
            }

                    // Tables
            public DbSet<User> users { get; set; }
            public DbSet<Course> courses { get; set; }
            public DbSet<Module> modules { get; set; }
            public DbSet<Challenge> challenges { get; set; }
    }
}