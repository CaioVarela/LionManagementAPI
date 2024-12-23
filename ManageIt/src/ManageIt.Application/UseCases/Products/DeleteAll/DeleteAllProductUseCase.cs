using ManageIt.Domain.Repositories;
using ManageIt.Domain.Repositories.Products;
using ManageIt.Exception;
using ManageIt.Exception.ExceptionBase;

namespace ManageIt.Application.UseCases.Products.Delete
{
    internal class DeleteAllProductUseCase : IDeleteAllProductUseCase
    {
        private readonly IProductWriteOnlyRepository _repository;
        private readonly IProductReadOnlyRepository _readOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteAllProductUseCase(IProductWriteOnlyRepository repository, IProductReadOnlyRepository readOnlyrepository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _readOnlyRepository = readOnlyrepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(Guid companyId)
        {
            var getAll = await _readOnlyRepository.GetAll();
            var productsToDelete = getAll.Where(p => p.CompanyId == companyId);

            var result = false;
            foreach (var product in productsToDelete) 
            {
                result = await _repository.Delete(product.Id);
            }

            if (result is false)
            {
                throw new NotFoundException(ResourceErrorMessages.COLLABORATOR_NOT_FOUND);
            }

            await _unitOfWork.Commit();
        }
    }
}
