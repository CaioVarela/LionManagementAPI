using ManageIt.Domain.Entities;

namespace ManageIt.Domain.Repositories.Collaborators
{
    public interface ICollaboratorWriteOnlyRepository
    {
        Task Add(Collaborator collaborator);
        Task<bool> Delete(Guid id);
    }
}
