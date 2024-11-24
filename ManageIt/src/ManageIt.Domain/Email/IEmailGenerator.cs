namespace ManageIt.Domain.Email
{
    public interface IEmailGenerator
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}
