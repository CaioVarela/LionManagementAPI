using ManageIt.Communication.ProductDTOs;

namespace ManageIt.Application.UseCases.Products.Update
{
    public interface IUpdateProductUseCase
    {
        Task Execute(Guid id, ProductDTO product, Guid companyId);
    }
}
