using AutoMapper;
using ManageIt.Communication.CompanyDTOs;
using ManageIt.Domain.Entities;
using ManageIt.Domain.Repositories;
using ManageIt.Domain.Repositories.Collaborators;
using ManageIt.Domain.Repositories.Companies;
using ManageIt.Domain.Repositories.Products;
using ManageIt.Domain.Repositories.User;

namespace ManageIt.Application.UseCases.Companies.Register
{
    public class RegisterCompanyUseCase : IRegisterCompanyUseCase
    {
        private readonly ICompanyWriteOnlyRepository _companyRepository;
        private readonly ICollaboratorReadOnlyRepository _collaboratorRepository;
        private readonly IProductReadOnlyRepository _productRepository;
        private readonly IUserReadOnlyRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterCompanyUseCase(
            ICompanyWriteOnlyRepository companyRepository,
            ICollaboratorReadOnlyRepository collaboratorRepository,
            IProductReadOnlyRepository productRepository,
            IUserReadOnlyRepository userRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _companyRepository = companyRepository;
            _collaboratorRepository = collaboratorRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CompanyDTO> Execute(CompanyDTO companyDTO)
        {
            var company = _mapper.Map<Company>(companyDTO);

            if (companyDTO.CollaboratorIds != null && companyDTO.CollaboratorIds.Any())
            {
                foreach (var collaboratorId in companyDTO.CollaboratorIds)
                {
                    var collaborator = await _productRepository.GetById(collaboratorId);
                    if (collaborator is not null)
                    {
                        company.Products.Add(collaborator);
                    }
                }
            }

            if (companyDTO.ProductIds != null && companyDTO.ProductIds.Any())
            {
                foreach (var productId in companyDTO.ProductIds)
                {
                    var product = await _productRepository.GetById(productId);
                    if (product is not null)
                    {
                        company.Products.Add(product);
                    }
                }
            }

            if (companyDTO.UserIds != null && companyDTO.UserIds.Any())
            {
                foreach (var userId in companyDTO.UserIds)
                {
                    var user = await _userRepository.GetById(userId);
                    if (user is not null)
                    {
                        company.Users.Add(user);
                    }
                }
            }

            await _companyRepository.Add(company);
            await _unitOfWork.Commit();

            return _mapper.Map<CompanyDTO>(company);
        }
    }
}
