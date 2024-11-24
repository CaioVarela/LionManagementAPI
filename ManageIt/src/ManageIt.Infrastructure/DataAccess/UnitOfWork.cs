using ManageIt.Domain.Repositories;

namespace ManageIt.Infrastructure.DataAccess
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly CollaboratorDbContext _dbContext;

        public UnitOfWork(CollaboratorDbContext context)
        {
            _dbContext = context;
        }

        public async Task Commit() => await _dbContext.SaveChangesAsync();
    }
}
