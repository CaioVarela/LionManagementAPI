using ManageIt.Communication.Requests;
using ManageIt.Communication.Responses;

namespace ManageIt.Application.UseCases.Users.Register
{
    public interface IRegisterUserUseCase
    {
        Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request);
    }
}
