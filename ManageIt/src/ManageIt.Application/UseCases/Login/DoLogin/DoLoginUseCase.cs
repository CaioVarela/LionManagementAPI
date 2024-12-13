using ManageIt.Communication.Requests;
using ManageIt.Communication.Responses;
using ManageIt.Domain.Repositories.Companies;
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
        private readonly ICompanyReadOnlyRepository _companyReadOnlyRepository;


        public DoLoginUseCase(IUserReadOnlyRepository repository, IPasswordEncripter passwordEncripter, IAccessTokenGenerator accessTokenGenerator, ICompanyReadOnlyRepository companyReadOnlyRepository)
        {
            _repository = repository;
            _passwordEncripter = passwordEncripter;
            _accessTokenGenerator = accessTokenGenerator;
            _companyReadOnlyRepository = companyReadOnlyRepository;
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

            var companyName = await _companyReadOnlyRepository.GetById(user.CompanyId);

            return new ResponseRegisteredUserJson
            {
                Name = user.UserName,
                Token = _accessTokenGenerator.Generate(user),
                Role = user.Role,
                CompanyName = companyName.Name
            };
        }
    }
}
