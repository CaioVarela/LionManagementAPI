using ManageIt.Communication.ProductDTOs;

namespace ManageIt.Application.UseCases.Products.Get.GetAllProducts
{
    public interface IGetAllProductsUseCase
    {
        Task<List<ProductDTO>> Execute();
    }
}
