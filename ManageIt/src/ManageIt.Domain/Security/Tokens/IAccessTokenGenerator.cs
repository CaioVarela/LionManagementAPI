using ManageIt.Domain.Entities;

namespace ManageIt.Domain.Security.Tokens
{
    public interface IAccessTokenGenerator
    {
        string Generate(User user);
    }
}
