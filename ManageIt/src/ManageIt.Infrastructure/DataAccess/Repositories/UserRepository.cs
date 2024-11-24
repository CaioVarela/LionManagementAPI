using ManageIt.Domain.Entities;
using ManageIt.Domain.Entities.Enums;
using ManageIt.Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace ManageIt.Infrastructure.DataAccess.Repositories
{
    internal class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository
    {
        private readonly CollaboratorDbContext _dbContext;

        public UserRepository(CollaboratorDbContext dbContext)  => _dbContext = dbContext;

        public async Task Add(User user)
        {
            await _dbContext.Users.AddAsync(user);
        }

        public async Task<bool> ExistActiveUserWithEmail(string email)
        {
            return await _dbContext.Users.AnyAsync(user => user.UserEmail.Equals(email));
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.UserEmail.Equals(email));
        }

        public async Task<User?> GetQualityManager()
        {
            return await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Role == Roles.QUALITY_MANAGER);
        }
    }
}
