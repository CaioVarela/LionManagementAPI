using ManageIt.Domain.Entities;

namespace ManageIt.Domain.Repositories.Collaborators
{
    public interface ICollaboratorUpdateOnlyRepository
    {
        Task<Collaborator?> GetById(Guid id);
        Task Update(Collaborator collaborator);
    }
}
