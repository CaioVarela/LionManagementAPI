using ManageIt.Communication.CollaboratorDTOs;

namespace ManageIt.Application.UseCases.Collaborators.Get.GetAllCollaborators
{
    public interface IGetAllCollaboratorsUseCase
    {
        Task<List<CollaboratorDTO>> Execute(Guid companyId);
    }
}
