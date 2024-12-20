using ManageIt.Communication.ProductDTOs;

namespace ManageIt.Application.UseCases.Products.Get.GetProductByName
{
    public interface IGetProductByNameUseCase
    {
        Task<List<ProductDTO>> Execute(string name, Guid companyId);
    }
}
