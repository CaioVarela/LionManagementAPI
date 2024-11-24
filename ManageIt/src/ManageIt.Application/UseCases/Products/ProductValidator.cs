using FluentValidation;
using ManageIt.Communication.ProductDTOs;
using ManageIt.Exception;

namespace ManageIt.Application.UseCases.Products
{
    public class ProductValidator : AbstractValidator<ProductDTO>
    {
        public ProductValidator() 
        {
            RuleFor(product => product.ProductName).NotEmpty().WithMessage(ResourceErrorMessages.NAME_Required);
            RuleFor(product => product.Balance).NotEmpty().WithMessage(ResourceErrorMessages.BALANCE_Required);
            RuleFor(product => product.MinimumStock).NotEmpty().WithMessage(ResourceErrorMessages.MINIMUM_STOCK_Required);
        }
    }
}
