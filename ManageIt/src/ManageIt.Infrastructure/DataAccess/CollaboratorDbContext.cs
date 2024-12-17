using ManageIt.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManageIt.Infrastructure.DataAccess
{
    internal class CollaboratorDbContext : DbContext
    {
        public CollaboratorDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Company> Companies { get; set; } = null!;
        public DbSet<Collaborator> Collaborators { get; set; } = null!;
        public DbSet<CollaboratorExam> CollaboratorExams { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .HasMany(c => c.Collaborators)
                .WithOne(c => c.Company)
                .HasForeignKey(c => c.CompanyId);

            modelBuilder.Entity<Company>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Company)
                .HasForeignKey(p => p.CompanyId);

            modelBuilder.Entity<Company>()
                .HasMany(c => c.Users)
                .WithOne(u => u.Company)
                .HasForeignKey(u => u.CompanyId);

            modelBuilder.Entity<Collaborator>().HasIndex(c => c.CompanyId);
            modelBuilder.Entity<Product>().HasIndex(p => p.CompanyId);
            modelBuilder.Entity<User>().HasIndex(u => u.CompanyId);
        }
    }
}
