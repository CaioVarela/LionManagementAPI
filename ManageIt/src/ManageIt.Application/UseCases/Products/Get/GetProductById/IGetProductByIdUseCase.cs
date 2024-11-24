using ManageIt.Communication.ProductDTOs;

namespace ManageIt.Application.UseCases.Products.Get.GetProductById
{
    public interface IGetProductByIdUseCase
    {
        Task<ProductDTO> Execute(Guid id);
    }
}
