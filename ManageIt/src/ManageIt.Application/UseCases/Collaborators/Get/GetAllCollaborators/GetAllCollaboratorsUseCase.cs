using AutoMapper;
using ManageIt.Communication.CollaboratorDTOs;
using ManageIt.Domain.Repositories.Collaborators;

namespace ManageIt.Application.UseCases.Collaborators.Get.GetAllCollaborators
{
    public class GetAllCollaboratorsUseCase : IGetAllCollaboratorsUseCase
    {
        private readonly ICollaboratorReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetAllCollaboratorsUseCase(ICollaboratorReadOnlyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CollaboratorDTO>> Execute()
        {
            var result = await _repository.GetAll();

            var test = _mapper.Map<List<CollaboratorDTO>>(result);

            return test;
        }
    }
}
