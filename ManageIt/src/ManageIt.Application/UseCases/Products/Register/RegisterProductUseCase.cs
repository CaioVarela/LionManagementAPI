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

        public async Task<ProductDTO> Execute(ProductDTO product)
        {
            var productMap = _mapper.Map<Product>(product);
            var productApprovalCertificationMap = _mapper.Map<ApprovalCertification>(product.ApprovalCertification);

            productMap.ApprovalCertification = productApprovalCertificationMap;

            await _repository.Add(productMap);
            await _unitOfWork.Commit();

            return _mapper.Map<ProductDTO>(productMap);
        }
    }
}
