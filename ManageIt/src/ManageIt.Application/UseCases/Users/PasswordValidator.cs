using FluentValidation;
using FluentValidation.Validators;
using ManageIt.Exception;
using System.Text.RegularExpressions;

namespace ManageIt.Application.UseCases.Users
{
    public partial class PasswordValidator<T> : PropertyValidator<T, string>
    {
        private const string ERROR_MESSAGE_KEY = "ErrorMessage";
        public override string Name => "PasswordValidator";

        protected override string GetDefaultMessageTemplate(string errorCode)
        {
            return $"{{{ERROR_MESSAGE_KEY}}}";
        }

        public override bool IsValid(ValidationContext<T> context, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            if(LowerCaseValidate().IsMatch(password) is false)
            {
                context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.INVALID_PASSWORD);
                return false;
            }

            if (UpperCaseValidate().IsMatch(password) is false)
            {
                context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.INVALID_PASSWORD);
                return false;
            }

            if (HasNumberValidate().IsMatch(password) is false)
            {
                context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.INVALID_PASSWORD);
                return false;
            }
            if (HasSimbolValidate().IsMatch(password) is false)
            {
                context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.INVALID_PASSWORD);
                return false;
            }

            return true;
        }

        [GeneratedRegex(@"[a-z]+")]
        private static partial Regex LowerCaseValidate();
        [GeneratedRegex(@"[A-Z]+")]
        private static partial Regex UpperCaseValidate();
        [GeneratedRegex(@"[0-9]+")]
        private static partial Regex HasNumberValidate();
        [GeneratedRegex(@"[\!\?\*\.]+")]
        private static partial Regex HasSimbolValidate();
    }
}
