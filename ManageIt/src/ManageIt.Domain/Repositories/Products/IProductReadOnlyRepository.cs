using ManageIt.Domain.Entities;

namespace ManageIt.Domain.Repositories.Products
{
    public interface IProductReadOnlyRepository
    {
        Task<List<Product>> GetAll();
        Task<Product?> GetById(Guid id);
        Task<List<Product?>> GetByName(string name);
    }
}
