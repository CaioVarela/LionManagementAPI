using AutoMapper;
using ManageIt.Communication.CompanyDTOs;
using ManageIt.Domain.Repositories.Companies;

namespace ManageIt.Application.UseCases.Companies.Get.GetAllCollaboratorsUseCase
{
    public class GetAllCompaniesUseCase : IGetAllCompaniesUseCase
    {
        private readonly ICompanyReadOnlyRepository _readOnlyCompanyRepository;
        private readonly IMapper _mapper;

        public GetAllCompaniesUseCase(ICompanyReadOnlyRepository readOnlyRepository, IMapper mapper)
        {
            _readOnlyCompanyRepository = readOnlyRepository;
            _mapper = mapper;
        }

        public async Task<List<CompanyDTO>> Execute()
        {
            var result = await _readOnlyCompanyRepository.GetAll();

            var response = _mapper.Map<List<CompanyDTO>>(result);

            return response;
        }
    }
}
