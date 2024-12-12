using ManageIt.Domain.Repositories;
using ManageIt.Domain.Repositories.Products;
using ManageIt.Exception;
using ManageIt.Exception.ExceptionBase;

namespace ManageIt.Application.UseCases.Products.Delete
{
    internal class DeleteAllProductUseCase : IDeleteAllProductUseCase
    {
        private readonly IProductWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteAllProductUseCase(IProductWriteOnlyRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute()
        {
            var result = await _repository.DeleteAll();

            if (result is false)
            {
                throw new NotFoundException(ResourceErrorMessages.COLLABORATOR_NOT_FOUND);
            }

            await _unitOfWork.Commit();
        }
    }
}
