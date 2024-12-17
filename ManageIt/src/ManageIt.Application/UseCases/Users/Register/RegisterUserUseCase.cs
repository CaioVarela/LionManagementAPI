using AutoMapper;
using ManageIt.Domain.Repositories.Collaborators;
using ManageIt.Domain.Repositories;
using ManageIt.Communication.Requests;
using ManageIt.Domain.Entities;
using ManageIt.Domain.Security.Cryptography;
using ManageIt.Application.UseCases.Collaborators;
using ManageIt.Exception.ExceptionBase;
using ManageIt.Domain.Repositories.User;
using ManageIt.Communication.Responses;
using FluentValidation.Results;
using ManageIt.Exception;
using ManageIt.Domain.Security.Tokens;
using ManageIt.Domain.Repositories.Companies;

namespace ManageIt.Application.UseCases.Users.Register
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUserReadOnlyRepository _userReadOnlyrepository;
        private readonly IUserWriteOnlyRepository _userWriteOnlyrepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPasswordEncripter _passwordEncripter;
        private readonly IAccessTokenGenerator _accessTokenGenerator;
        private readonly ICompanyReadOnlyRepository _companyReadOnlyrepository;

        public RegisterUserUseCase(IUserReadOnlyRepository userReadOnlyrepository, IUserWriteOnlyRepository userWriteOnlyRepository, IUnitOfWork unitOfWork, IMapper mapper, IPasswordEncripter passwordEncripter, IAccessTokenGenerator accessTokenGenerator, ICompanyReadOnlyRepository companyReadOnlyRepository)
        {
            _userReadOnlyrepository = userReadOnlyrepository;
            _userWriteOnlyrepository = userWriteOnlyRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _passwordEncripter = passwordEncripter;
            _accessTokenGenerator = accessTokenGenerator;
            _companyReadOnlyrepository = companyReadOnlyRepository;
        }

        public async Task<ResponseRegisteredUserJson>Execute (RequestRegisterUserJson request)
        {
            await Validate(request);

            var company = await _companyReadOnlyrepository.GetById(request.CompanyId);

            var userMap = _mapper.Map<User>(request);
            userMap.PasswordHash = _passwordEncripter.Encrypt(request.Password);
            userMap.Id = new Guid();
            userMap.CompanyId = company!.Id;

            await _userWriteOnlyrepository.Add(userMap);
            await _unitOfWork.Commit();

            return new ResponseRegisteredUserJson
            {
                Name = userMap.UserName,
                Token = _accessTokenGenerator.Generate(userMap),
                Role = userMap.Role,
                CompanyName = company.Name,
            };
        }

        private async Task Validate(RequestRegisterUserJson request)
        {
            var result = new UserValidator().Validate(request);
            var emailExists = await _userReadOnlyrepository.ExistActiveUserWithEmail(request.UserEmail);
            if(emailExists)
            {
                result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_ALREADY_REGISTERED));
            }

            var companyExists = await _companyReadOnlyrepository.GetById(request.CompanyId);
            if(companyExists is null)
            {
                result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.COMPANY_Invalid));
            }

            if (result.IsValid is false)
            {
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
