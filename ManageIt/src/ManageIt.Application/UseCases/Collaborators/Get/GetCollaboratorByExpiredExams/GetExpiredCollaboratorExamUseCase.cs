using AutoMapper;
using ManageIt.Communication.CollaboratorDTOs;
using ManageIt.Communication.Responses;
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

        public async Task<ResponseGetAllCollaboratorsWithExpiringSoonExams> Execute()
        {
            var expiredCollaborators = await _repository.GetExpired();

            var allCollaborators = await _repository.GetAll();

            var collaboratorDTO = _mapper.Map<List<CollaboratorDTO>>(expiredCollaborators);

            var expiredCollaboratorsCount = expiredCollaborators.Count;
            
            var collaboratorsWithExpiredExamsPercentage = ((double)expiredCollaboratorsCount / allCollaborators.Count) * 100;

            var resultDTO = new ResponseGetAllCollaboratorsWithExpiringSoonExams()
            {
                Collaborator = collaboratorDTO,
                CollaboratorsCount = allCollaborators.Count,
                CollaboratorsPercentage = (float)collaboratorsWithExpiredExamsPercentage
            };

            return resultDTO;
        }
    }
}
