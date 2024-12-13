using ManageIt.Domain.Entities;

namespace ManageIt.Domain.Repositories.Companies
{
    public interface ICompanyReadOnlyRepository
    {
        Task<List<Company>> GetAll();
        Task<Company?> GetById(Guid id);
        Task<Company?> GetByName(string name);
    }
}
