using ManageIt.Domain.Entities;
using ManageIt.Domain.Repositories.Collaborators;
using Microsoft.EntityFrameworkCore;

namespace ManageIt.Infrastructure.DataAccess.Repositories
{
    internal class CollaboratorsRepository : ICollaboratorWriteOnlyRepository, ICollaboratorReadOnlyRepository, ICollaboratorUpdateOnlyRepository
    {
        private readonly CollaboratorDbContext _dbContext;

        public CollaboratorsRepository(CollaboratorDbContext context)
        {
            _dbContext = context;
        }

        public async Task Add(Collaborator collaborator)
        {
            _dbContext.Collaborators.Add(collaborator);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddUser(User user)
        {
            await _dbContext.Users.AddAsync(user);
        }

        public async Task<bool> Delete(Guid id)
        {
            var result = await _dbContext.Collaborators.FirstOrDefaultAsync(c => c.Id == id);
            
            if (result is null)
            {
                return false;
            }

            _dbContext.Collaborators.Remove(result);

            return true;
        }

        public async Task Update(Collaborator collaborator)
        {
            _dbContext.Collaborators.Update(collaborator);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Collaborator>> GetAll()
        {
            return await _dbContext.Collaborators.AsNoTracking().Include(c => c.Exams).ToListAsync();
        }

        async Task<Collaborator?> ICollaboratorReadOnlyRepository.GetById(Guid id)
        {
            return await _dbContext.Collaborators
                .Include(c => c.Exams)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        
        async Task<Collaborator?> ICollaboratorUpdateOnlyRepository.GetById(Guid id)
        {
            return await _dbContext.Collaborators
                .Include(c => c.Exams)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Collaborator?>?> GetExpiringSoon()
        {
            var collaborators = await _dbContext.Collaborators.AsNoTracking().Include(c => c.Exams).ToListAsync();
            return collaborators.Where(c =>
                c.Exams.Any(e => e.IsExpiringSoon)
            ).ToList();
        }

        public async Task<List<Collaborator?>> GetExpired()
        {
            var collaborators = await _dbContext.Collaborators.AsNoTracking().Include(c => c.Exams).ToListAsync();
            return collaborators.Where(c =>
                c.Exams.Any(e => e.IsExpired)
            ).ToList();
        }

        public async Task<Collaborator?> GetByName(string name)
        {
            var collaborators = await _dbContext.Collaborators.AsNoTracking().Include(c => c.Exams).ToListAsync();
            return collaborators.Where(c => c.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
        }
    }
}
