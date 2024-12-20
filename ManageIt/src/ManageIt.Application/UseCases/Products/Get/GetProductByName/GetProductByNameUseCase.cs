using AutoMapper;
using ManageIt.Application.UseCases.Products.Get.GetProductByName;
using ManageIt.Communication.ProductDTOs;
using ManageIt.Domain.Entities;
using ManageIt.Domain.Repositories.Products;

namespace ManageIt.Application.UseCases.Products.Get.GetProductById
{
    public class GetProductByNameUseCase : IGetProductByNameUseCase
    {
        private readonly IProductReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetProductByNameUseCase(IProductReadOnlyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ProductDTO>> Execute(string  name, Guid companyId)
        {
            var getByNameResult = await _repository.GetByName(name);
            Product? result;

            if (getByNameResult?.CompanyId == companyId)
            {
                result = getByNameResult;
            }
            else 
            {
                result = null;
            }
            var resultDTO = _mapper.Map<List<ProductDTO>>(result);

            return resultDTO;
        }
    }
}
