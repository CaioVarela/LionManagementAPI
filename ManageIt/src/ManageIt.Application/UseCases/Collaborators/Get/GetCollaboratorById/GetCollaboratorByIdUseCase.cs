using AutoMapper;
using ManageIt.Communication.CollaboratorDTOs;
using ManageIt.Domain.Repositories.Collaborators;
using ManageIt.Exception;
using ManageIt.Exception.ExceptionBase;

namespace ManageIt.Application.UseCases.Collaborators.Get.GetCollaboratorById
{
    public class GetCollaboratorByIdUseCase : IGetCollaboratorByIdUseCase
    {
        private readonly ICollaboratorReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetCollaboratorByIdUseCase(ICollaboratorReadOnlyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CollaboratorDTO> Execute(Guid id)
        {
            var result = await _repository.GetById(id);

            if(result is null)
            {
                throw new NotFoundException(ResourceErrorMessages.COLLABORATOR_NOT_FOUND);
            }

            var resultDTO = _mapper.Map<CollaboratorDTO>(result);

            return resultDTO;
        }
    }
}
