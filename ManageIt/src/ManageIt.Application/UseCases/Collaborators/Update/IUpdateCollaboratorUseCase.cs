using ManageIt.Communication.CollaboratorDTOs;

namespace ManageIt.Application.UseCases.Collaborators.Update
{
    public interface IUpdateCollaboratorUseCase
    {
        Task Execute(Guid id, CollaboratorDTO collaborator);
    }
}
