using ManageIt.Domain.Entities;

namespace ManageIt.Domain.Repositories.Companies
{
    public interface ICompanyWriteOnlyRepository
    {
        Task Add(Company company);
        Task<bool> Delete(Guid id);
    }
}
