using FluentValidation;
using ManageIt.Application.UseCases.Users;
using ManageIt.Communication.Requests;
using ManageIt.Domain.Entities;
using ManageIt.Exception;

namespace ManageIt.Application.UseCases.Collaborators
{
    public class UserValidator : AbstractValidator<RequestRegisterUserJson>
    {
        public UserValidator()
        {
            RuleFor(user => user.UserName).NotEmpty().WithMessage(ResourceErrorMessages.NAME_Required);
            RuleFor(user => user.UserEmail).NotEmpty().WithMessage(ResourceErrorMessages.EMAIL_Required).EmailAddress().WithMessage(ResourceErrorMessages.EMAIL_Invalid);

            RuleFor(user => user.Password).SetValidator(new PasswordValidator<RequestRegisterUserJson>());
        }
    }
}
