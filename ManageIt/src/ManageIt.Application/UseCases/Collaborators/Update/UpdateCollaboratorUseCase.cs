using AutoMapper;
using ManageIt.Application.UseCases.Collaborators.Update;
using ManageIt.Communication.CollaboratorDTOs;
using ManageIt.Domain.Repositories;
using ManageIt.Domain.Repositories.Collaborators;
using ManageIt.Exception;
using ManageIt.Exception.ExceptionBase;

namespace ManageIt.Application.UseCases.Collaborators.Register
{
    public class UpdateCollaboratorUseCase : IUpdateCollaboratorUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICollaboratorUpdateOnlyRepository _repository;
        private readonly IMapper _mapper;

        public UpdateCollaboratorUseCase(ICollaboratorUpdateOnlyRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Execute(Guid id, CollaboratorDTO collaborator)
        {
            Validate(collaborator);

            var collaboratorToUpdate = await _repository.GetById(id);

            if (collaboratorToUpdate == null) 
            {
                throw new NotFoundException(ResourceErrorMessages.COLLABORATOR_NOT_FOUND);
            }

            _mapper.Map(collaborator, collaboratorToUpdate);

            _repository.Update(collaboratorToUpdate);

            await _unitOfWork.Commit();
        }

        private void Validate(CollaboratorDTO collaborator)
        {
            var validator = new CollaboratorValidator();

            var result = validator.Validate(collaborator);

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
