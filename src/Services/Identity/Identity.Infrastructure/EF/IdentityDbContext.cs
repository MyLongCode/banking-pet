using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.EF
{
    public class IdentityDBContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public IdentityDBContext(DbContextOptions<IdentityDBContext> options)
            : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(e =>
            {
                e.HasKey(e => e.Id);
                e.HasAlternateKey(e => e.FullName);
                e.HasAlternateKey(e => e.PhoneNumber);
                e.HasAlternateKey(e => e.Email);
            });
        }
    }
}
