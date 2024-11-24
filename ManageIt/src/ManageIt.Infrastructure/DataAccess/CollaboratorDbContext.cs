using ManageIt.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManageIt.Infrastructure.DataAccess
{
    internal class CollaboratorDbContext : DbContext
    {
        public CollaboratorDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Collaborator> Collaborators { get; set; }
        public DbSet<CollaboratorExam> CollaboratorExams { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
