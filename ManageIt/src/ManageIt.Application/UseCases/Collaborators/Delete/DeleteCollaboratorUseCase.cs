using ManageIt.Domain.Repositories;
using ManageIt.Domain.Repositories.Collaborators;
using ManageIt.Exception;
using ManageIt.Exception.ExceptionBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageIt.Application.UseCases.Collaborators.Delete
{
    internal class DeleteCollaboratorUseCase : IDeleteCollaboratorUseCase
    {
        private readonly ICollaboratorWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCollaboratorUseCase(ICollaboratorWriteOnlyRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(Guid id)
        {
            var result = await _repository.Delete(id);

            if (result is false)
            {
                throw new NotFoundException(ResourceErrorMessages.COLLABORATOR_NOT_FOUND);
            }

            await _unitOfWork.Commit();
        }
    }
}
