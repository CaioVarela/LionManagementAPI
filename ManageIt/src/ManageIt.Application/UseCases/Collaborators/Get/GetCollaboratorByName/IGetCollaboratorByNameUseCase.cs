using ManageIt.Communication.CollaboratorDTOs;

namespace ManageIt.Application.UseCases.Collaborators.Get.GetCollaboratorByName
{
    public interface IGetCollaboratorByNameUseCase
    {
        Task<List<CollaboratorDTO>> Execute(string name);
    }
}
