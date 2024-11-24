using ManageIt.Domain.Entities;

namespace ManageIt.Domain.Repositories.Collaborators
{
    public interface ICollaboratorReadOnlyRepository
    {
        Task<List<Collaborator>> GetAll();
        Task<Collaborator?> GetById(Guid id);
        Task<Collaborator?> GetByName(string name);
        Task<List<Collaborator?>> GetExpired();
        Task<List<Collaborator?>?> GetExpiringSoon();
    }
}
