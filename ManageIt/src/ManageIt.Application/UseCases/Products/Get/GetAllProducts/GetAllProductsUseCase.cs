using AutoMapper;
using ManageIt.Communication.ProductDTOs;
using ManageIt.Domain.Repositories.Products;

namespace ManageIt.Application.UseCases.Products.Get.GetAllProducts
{
    public class GetAllProductsUseCase : IGetAllProductsUseCase
    {
        private readonly IProductReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetAllProductsUseCase(IProductReadOnlyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ProductDTO>> Execute()
        {
            var result = await _repository.GetAll();

            var test = _mapper.Map<List<ProductDTO>>(result);

            return test;
        }
    }
}
