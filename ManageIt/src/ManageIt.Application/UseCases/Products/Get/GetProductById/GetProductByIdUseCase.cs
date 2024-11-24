using AutoMapper;
using ManageIt.Communication.ProductDTOs;
using ManageIt.Domain.Repositories.Products;
using ManageIt.Exception;
using ManageIt.Exception.ExceptionBase;

namespace ManageIt.Application.UseCases.Products.Get.GetProductById
{
    public class GetProductByIdUseCase : IGetProductByIdUseCase
    {
        private readonly IProductReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetProductByIdUseCase(IProductReadOnlyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProductDTO> Execute(Guid id)
        {
            var result = await _repository.GetById(id);

            if(result is null)
            {
                throw new NotFoundException(ResourceErrorMessages.COLLABORATOR_NOT_FOUND);
            }

            var resultDTO = _mapper.Map<ProductDTO>(result);

            return resultDTO;
        }
    }
}
