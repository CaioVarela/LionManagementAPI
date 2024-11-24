
namespace ManageIt.Application.UseCases.Collaborators.Delete
{
    public interface IDeleteCollaboratorUseCase
    {
        Task Execute(Guid id);
    }
}
