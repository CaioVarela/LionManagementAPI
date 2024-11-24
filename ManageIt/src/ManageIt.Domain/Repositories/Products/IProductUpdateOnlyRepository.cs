using ManageIt.Domain.Entities;

namespace ManageIt.Domain.Repositories.Products
{
    public interface IProductUpdateOnlyRepository
    {
        Task<Product?> GetById(Guid id);
        void Update(Product product);
    }
}
