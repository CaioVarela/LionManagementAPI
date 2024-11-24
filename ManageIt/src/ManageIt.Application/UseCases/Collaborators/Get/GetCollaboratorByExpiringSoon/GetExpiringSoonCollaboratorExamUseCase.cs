using AutoMapper;
using ManageIt.Application.UseCases.Collaborators.Get.GetCollaboratorByExpiringSoon;
using ManageIt.Communication.CollaboratorDTOs;
using ManageIt.Domain.Repositories.Collaborators;

namespace ManageIt.Application.UseCases.Collaborators.Get.GetCollaboratorByExpiredExams
{
    public class GetExpiringSoonCollaboratorExamUseCase : IGetExpiringSoonCollaboratorExamUseCase
    {
        private readonly ICollaboratorReadOnlyRepository _repository;
        private readonly IMapper _mapper;
        public GetExpiringSoonCollaboratorExamUseCase(ICollaboratorReadOnlyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CollaboratorDTO>> Execute()
        {
            var result = await _repository.GetExpiringSoon();

            var resultDTO = _mapper.Map<List<CollaboratorDTO>>(result);

            return resultDTO;
        }
    }
}
