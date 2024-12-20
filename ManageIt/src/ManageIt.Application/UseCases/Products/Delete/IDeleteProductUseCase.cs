
namespace ManageIt.Application.UseCases.Products.Delete
{
    public interface IDeleteProductUseCase
    {
        Task Execute(Guid id, Guid companyId);
    }
}
