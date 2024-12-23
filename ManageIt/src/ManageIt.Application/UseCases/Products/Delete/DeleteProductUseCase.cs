using ManageIt.Domain.Repositories;
using ManageIt.Domain.Repositories.Products;
using ManageIt.Exception;
using ManageIt.Exception.ExceptionBase;

namespace ManageIt.Application.UseCases.Products.Delete
{
    internal class DeleteProductUseCase : IDeleteProductUseCase
    {
        private readonly IProductWriteOnlyRepository _repository;
        private readonly IProductReadOnlyRepository _readOnlyrepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteProductUseCase(IProductWriteOnlyRepository repository, IProductReadOnlyRepository readOnlyrepository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _readOnlyrepository = readOnlyrepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(Guid id, Guid companyId)
        {
            var getById = await _readOnlyrepository.GetById(id);

            if (getById.CompanyId != companyId)
            {
                throw new NotFoundException(ResourceErrorMessages.COLLABORATOR_NOT_FOUND);
            }

            var result = await _repository.Delete(id);

            if (result is false)
            {
                throw new NotFoundException(ResourceErrorMessages.COLLABORATOR_NOT_FOUND);
            }

            await _unitOfWork.Commit();
        }
    }
}
