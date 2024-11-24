using ManageIt.Communication.CollaboratorDTOs;

namespace ManageIt.Application.UseCases.Collaborators.Register
{
    public interface IRegisterCollaboratorUseCase
    {
        Task<CollaboratorDTO> Execute(CollaboratorDTO collaborator);
    }
}
