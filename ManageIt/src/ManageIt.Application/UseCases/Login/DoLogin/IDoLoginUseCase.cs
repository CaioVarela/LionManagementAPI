using ManageIt.Communication.Requests;
using ManageIt.Communication.Responses;

namespace ManageIt.Application.UseCases.Login.DoLogin
{
    public interface IDoLoginUseCase
    {
        public Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request);
    }
}
