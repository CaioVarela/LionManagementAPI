namespace ManageIt.Api.Services
{
    public interface ICurrentUserService
    {
        Guid? GetCurrentUserId();
        Guid? GetCurrentCompanyId();
        string? GetCurrentUserEmail();
        bool IsAuthenticated();
    }
}
