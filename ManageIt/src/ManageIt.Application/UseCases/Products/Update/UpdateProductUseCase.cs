using AutoMapper;
using ManageIt.Application.UseCases.Products.Update;
using ManageIt.Communication.ProductDTOs;
using ManageIt.Domain.Repositories;
using ManageIt.Domain.Repositories.Products;
using ManageIt.Exception;
using ManageIt.Exception.ExceptionBase;

namespace ManageIt.Application.UseCases.Products.Register
{
    public class UpdateProductUseCase : IUpdateProductUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductUpdateOnlyRepository _repository;
        private readonly IMapper _mapper;

        public UpdateProductUseCase(IProductUpdateOnlyRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Execute(Guid id, ProductDTO product, Guid companyId)
        {
            Validate(product);

            var productToUpdate = await _repository.GetById(id);

            if (productToUpdate == null) 
            {
                throw new NotFoundException(ResourceErrorMessages.COLLABORATOR_NOT_FOUND);
            }
            else if (productToUpdate.CompanyId != companyId)
            {
                throw new NotFoundException(ResourceErrorMessages.COLLABORATOR_NOT_FOUND);
            }

            _mapper.Map(product, productToUpdate);

            _repository.Update(productToUpdate);

            await _unitOfWork.Commit();
        }

        private void Validate(ProductDTO product)
        {
            var validator = new ProductValidator();

            var result = validator.Validate(product);

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
