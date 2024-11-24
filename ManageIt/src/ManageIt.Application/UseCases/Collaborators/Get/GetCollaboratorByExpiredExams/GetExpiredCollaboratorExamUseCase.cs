using AutoMapper;
using ManageIt.Communication.CollaboratorDTOs;
using ManageIt.Domain.Repositories.Collaborators;

namespace ManageIt.Application.UseCases.Collaborators.Get.GetCollaboratorByExpiredExams
{
    public class GetExpiredCollaboratorExamUseCase : IGetExpiredCollaboratorExamUseCase
    {
        private readonly ICollaboratorReadOnlyRepository _repository;
        private readonly IMapper _mapper;
        public GetExpiredCollaboratorExamUseCase(ICollaboratorReadOnlyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CollaboratorDTO>> Execute()
        {
            var result = await _repository.GetExpired();

            var resultDTO = _mapper.Map<List<CollaboratorDTO>>(result);

            return resultDTO;
        }
    }
}
