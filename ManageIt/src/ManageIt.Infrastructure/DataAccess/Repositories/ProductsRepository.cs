using ManageIt.Domain.Entities;
using ManageIt.Domain.Repositories.Products;
using Microsoft.EntityFrameworkCore;

namespace ManageIt.Infrastructure.DataAccess.Repositories
{
    internal class ProductsRepository : IProductWriteOnlyRepository, IProductReadOnlyRepository, IProductUpdateOnlyRepository
    {
        private readonly CollaboratorDbContext _dbContext;

        public ProductsRepository(CollaboratorDbContext context)
        {
            _dbContext = context;
        }

        public async Task Add(Product product)
        {
            await _dbContext.Products.AddAsync(product);
        }

        public async Task<bool> Delete(Guid id)
        {
            var result = await _dbContext.Products.FirstOrDefaultAsync(c => c.Id == id);

            if (result is null)
            {
                return false;
            }

            _dbContext.Products.Remove(result);

            return true;
        }

        public void Update(Product product)
        {
            _dbContext.Products.Update(product);
        }

        public async Task<List<Product>> GetAll()
        {
            return await _dbContext.Products.AsNoTracking().Include(c => c.ApprovalCertification).ToListAsync();
        }

        async Task<Product?> IProductReadOnlyRepository.GetById(Guid id)
        {
            return await _dbContext.Products
                .Include(c => c.ApprovalCertification)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        async Task<Product?> IProductUpdateOnlyRepository.GetById(Guid id)
        {
            return await _dbContext.Products
                .Include(c => c.ApprovalCertification)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Product?>> GetByName(string name)
        {
            var collaborators = await _dbContext.Products.AsNoTracking().Include(c => c.ApprovalCertification).ToListAsync();
            return collaborators.Where(c => c.ProductName.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}
