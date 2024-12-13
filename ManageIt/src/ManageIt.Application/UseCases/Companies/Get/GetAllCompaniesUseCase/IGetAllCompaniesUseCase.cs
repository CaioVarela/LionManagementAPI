using ManageIt.Communication.CompanyDTOs;

namespace ManageIt.Application.UseCases.Companies.Get.GetAllCollaboratorsUseCase
{
    public interface IGetAllCompaniesUseCase
    {
        Task<List<CompanyDTO>> Execute();
    }
}
