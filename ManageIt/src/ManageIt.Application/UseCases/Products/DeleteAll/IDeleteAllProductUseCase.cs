
namespace ManageIt.Application.UseCases.Products.Delete
{
    public interface IDeleteAllProductUseCase
    {
        Task Execute(Guid companyId);
    }
}
