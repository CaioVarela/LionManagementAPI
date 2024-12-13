using ManageIt.Domain.Entities;
using ManageIt.Domain.Repositories.Companies;
using Microsoft.EntityFrameworkCore;

namespace ManageIt.Infrastructure.DataAccess.Repositories
{
    internal class CompaniesRepository : ICompanyReadOnlyRepository, ICompanyWriteOnlyRepository, ICompanyUpdateOnlyRepository
    {
        private readonly CollaboratorDbContext _dbContext;

        public CompaniesRepository(CollaboratorDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Company>> GetAll()
        {
            return await _dbContext.Companies.AsNoTracking().Include(c => c.Users).Include(c => c.Products).Include(c => c.Collaborators).ToListAsync();
        }

        public async Task<Company?> GetById(Guid id)
        {
            return await _dbContext.Companies.AsNoTracking().Include(c => c.Users).Include(c => c.Products).Include(c => c.Collaborators).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Company?> GetByName(string name)
        {
            return await _dbContext.Companies.AsNoTracking().Include(c => c.Users).FirstOrDefaultAsync(c => c.Name == name);
        }

        public Task Add(Company company)
        {
            _dbContext.Companies.Add(company);
            return Task.CompletedTask;
        }

        public async Task<bool> Delete(Guid id)
        {
            var result = await _dbContext.Companies.FirstOrDefaultAsync(c => c.Id == id);

            if (result is null)
            {
                return false;
            }

            _dbContext.Companies.Remove(result);

            return true;
        }

        public Task Update(Company company)
        {
            _dbContext.Companies.Update(company);
            return Task.CompletedTask;
        }
    }
}
