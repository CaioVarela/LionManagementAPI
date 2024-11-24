using ManageIt.Communication.Requests;
using ManageIt.Communication.Responses;
using ManageIt.Domain.Repositories.User;
using ManageIt.Domain.Security.Cryptography;
using ManageIt.Domain.Security.Tokens;
using ManageIt.Exception.ExceptionBase;

namespace ManageIt.Application.UseCases.Login.DoLogin
{
    public class DoLoginUseCase : IDoLoginUseCase
    {
        private readonly IUserReadOnlyRepository _repository;
        private readonly IPasswordEncripter _passwordEncripter;
        private readonly IAccessTokenGenerator _accessTokenGenerator;


        public DoLoginUseCase(IUserReadOnlyRepository repository, IPasswordEncripter passwordEncripter, IAccessTokenGenerator accessTokenGenerator)
        {
            _repository = repository;
            _passwordEncripter = passwordEncripter;
            _accessTokenGenerator = accessTokenGenerator;
        }

        public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
        {
            var user = await _repository.GetUserByEmail(request.Email);

            if (user is null)
            {
                throw new InvalidLoginException();
            }

            var passwordMatch = _passwordEncripter.Verify(request.Password, user.PasswordHash);

            if (passwordMatch is false)
            {
                throw new InvalidLoginException();
            }

            return new ResponseRegisteredUserJson
            {
                Name = user.UserName,
                Token = _accessTokenGenerator.Generate(user)
            };
        }
    }
}
