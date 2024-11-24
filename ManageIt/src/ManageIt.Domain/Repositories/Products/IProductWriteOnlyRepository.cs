using ManageIt.Domain.Entities;

namespace ManageIt.Domain.Repositories.Products
{
    public interface IProductWriteOnlyRepository
    {
        Task Add(Product product);
        Task<bool> Delete(Guid id);
    }
}
