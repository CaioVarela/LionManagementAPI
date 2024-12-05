using ManageIt.Communication.Responses;

namespace ManageIt.Application.UseCases.Products.Get.GetAllProducts
{
    public interface IGetAllProductsUseCase
    {
        Task<ResponseGetAllProducts> Execute();
    }
}
