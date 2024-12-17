using ManageIt.Communication.CompanyDTOs;

namespace ManageIt.Application.UseCases.Companies.Register
{
    public interface IRegisterCompanyUseCase
    {
        Task<CompanyDTO> Execute(CompanyDTO companyDTO);
    }
}
