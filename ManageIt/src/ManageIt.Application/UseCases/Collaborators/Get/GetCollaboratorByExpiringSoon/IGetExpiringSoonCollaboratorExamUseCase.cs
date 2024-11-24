using ManageIt.Communication.CollaboratorDTOs;

namespace ManageIt.Application.UseCases.Collaborators.Get.GetCollaboratorByExpiringSoon
{
    public interface IGetExpiringSoonCollaboratorExamUseCase
    {
        Task<List<CollaboratorDTO>> Execute();
    }
}
