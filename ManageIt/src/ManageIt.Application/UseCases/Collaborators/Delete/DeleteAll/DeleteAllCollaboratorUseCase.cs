using ManageIt.Domain.Repositories;
using ManageIt.Domain.Repositories.Collaborators;
using ManageIt.Exception;
using ManageIt.Exception.ExceptionBase;

namespace ManageIt.Application.UseCases.Collaborators.Delete.DeleteAll
{
    internal class DeleteAllCollaboratorUseCase : IDeleteAllCollaboratorUseCase
    {
        private readonly ICollaboratorWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteAllCollaboratorUseCase(ICollaboratorWriteOnlyRepository repository, IUnitOfWork unitOfWork)
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
