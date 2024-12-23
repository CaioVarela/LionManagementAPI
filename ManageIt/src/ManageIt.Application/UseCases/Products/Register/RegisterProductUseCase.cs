using AutoMapper;
using ManageIt.Communication.ProductDTOs;
using ManageIt.Domain.Entities;
using ManageIt.Domain.Repositories;
using ManageIt.Domain.Repositories.Products;

namespace ManageIt.Application.UseCases.Products.Register
{
    public class RegisterProductUseCase : IRegisterProductUseCase
    {
        private readonly IProductWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterProductUseCase(IProductWriteOnlyRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductDTO> Execute(ProductDTO product, Guid companyId)
        {
            var productMapped = _mapper.Map<Product>(product);
            var productApprovalCertificationMap = _mapper.Map<ApprovalCertification>(product.ApprovalCertification);

            productMapped.ApprovalCertification = productApprovalCertificationMap;

            productMapped.CompanyId = companyId;

            await _repository.Add(productMapped);
            await _unitOfWork.Commit();

            return _mapper.Map<ProductDTO>(productMapped);
        }
    }
}
