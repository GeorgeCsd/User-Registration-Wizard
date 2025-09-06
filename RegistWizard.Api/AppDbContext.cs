using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RegistWizard.Api.Models;

namespace RegistWizard.Api
{
    public class AppDbContext : IdentityDbContext<AppUser>

    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions)
        : base(dbContextOptions)
        {

        }

        public DbSet<Industry> Industries => Set<Industry>();

        public DbSet<Company> Companies => Set<Company>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Industry>().Property(i => i.Name).IsRequired().HasMaxLength(120);
            modelBuilder.Entity<Company>().Property(c => c.Name).IsRequired().HasMaxLength(120);

            modelBuilder.Entity<Company>().HasOne(i => i.Industry).WithMany(c => c.Companies).HasForeignKey(i => i.IndustryId);
            modelBuilder.Entity<AppUser>().HasOne(c => c.Company).WithMany(c => c.Users).HasForeignKey(c => c.CompanyId);
        }
        
    }
}