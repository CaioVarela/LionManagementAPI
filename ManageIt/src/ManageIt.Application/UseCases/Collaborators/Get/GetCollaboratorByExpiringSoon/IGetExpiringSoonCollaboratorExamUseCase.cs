using ManageIt.Communication.CollaboratorDTOs;
using ManageIt.Communication.Responses;

namespace ManageIt.Application.UseCases.Collaborators.Get.GetCollaboratorByExpiringSoon
{
    public interface IGetExpiringSoonCollaboratorExamUseCase
    {
        Task<ResponseGetAllCollaboratorsWithExpiringSoonExams> Execute();
    }
}
