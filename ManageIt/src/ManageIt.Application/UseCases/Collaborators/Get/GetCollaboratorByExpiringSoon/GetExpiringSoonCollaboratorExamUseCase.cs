using AutoMapper;
using ManageIt.Application.UseCases.Collaborators.Get.GetCollaboratorByExpiringSoon;
using ManageIt.Communication.CollaboratorDTOs;
using ManageIt.Communication.Responses;
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

        public async Task<ResponseGetAllCollaboratorsWithExpiringSoonExams> Execute(Guid companyId)
        {
            var getExpiringSoon = await _repository.GetExpiringSoon();
            var result = getExpiringSoon.Where(c => c.CompanyId == companyId);

            var getAllCollaborators = await _repository.GetAll();
            var allCollaborators = getAllCollaborators.Where(c => c.CompanyId == companyId);

            var collaboratorDTOs = _mapper.Map<List<CollaboratorDTO>>(result);

            var expiringCollaboratorsCount = collaboratorDTOs.Count();

            var asoExpiring = collaboratorDTOs.Count(c => c.Exams is not null && c.Exams.Any(e => e.ExamName == "ASO"));
            var harExpiring = collaboratorDTOs.Count(c => c.Exams is not null && c.Exams.Any(e => e.ExamName == "HAR"));
            var nr10Expiring = collaboratorDTOs.Count(c => c.Exams is not null && c.Exams.Any(e => e.ExamName == "NR10"));
            var nr35Expiring = collaboratorDTOs.Count(c => c.Exams is not null && c.Exams.Any(e => e.ExamName == "NR35"));
            var avaliacaoPsicologicaExpiring = collaboratorDTOs.Count(c => c.Exams is not null && c.Exams.Any(e => e.ExamName == "Avaliacao Psicologica"));
            var direcaoDefensivaExpiring = collaboratorDTOs.Count(c => c.Exams is not null && c.Exams.Any(e => e.ExamName == "Direcao Defensiva"));
            var cnhExpiring = collaboratorDTOs.Count(c => c.Exams is not null && c.Exams.Any(e => e.ExamName == "CNH"));

            double collaboratorsWithExpiringSoonExamsPercentage = ((double)expiringCollaboratorsCount / allCollaborators.Count()) * 100;

            var resultDTO = new ResponseGetAllCollaboratorsWithExpiringSoonExams()
            {
                Collaborator = collaboratorDTOs,
                CollaboratorsCount = expiringCollaboratorsCount,
                CollaboratorsPercentage = (float)collaboratorsWithExpiringSoonExamsPercentage,
                AsoExpiring = asoExpiring,
                AvaliacaoPsicologicaExpiring = avaliacaoPsicologicaExpiring,
                CnhExpiring = cnhExpiring,
                DirecaoDefensivaExpiring = direcaoDefensivaExpiring,
                HarExpiring = harExpiring,
                Nr10Expiring = nr10Expiring,
                Nr35Expiring = nr35Expiring
            };

            return resultDTO;
        }
    }
}
