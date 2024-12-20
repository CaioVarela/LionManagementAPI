using ManageIt.Communication.ProductDTOs;

namespace ManageIt.Application.UseCases.Products.Register
{
    public interface IRegisterProductUseCase
    {
        Task<ProductDTO> Execute(ProductDTO collaborator, Guid companyId);
    }
}
