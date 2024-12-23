using ManageIt.Domain.Entities;

namespace ManageIt.Domain.Repositories.Companies
{
    public interface ICompanyUpdateOnlyRepository
    {
        Task Update(Company company);
    }
}
