



using ManageIt.Domain.Email;

namespace ManageIt.Application.UseCases.Email
{
    public class SendEmailUseCase : ISendEmailUseCase
    {
        private readonly IEmailGenerator _emailGenerator;

        public SendEmailUseCase(IEmailGenerator emailGenerator)
        {
            _emailGenerator = emailGenerator;
        }

        public async Task Execute(string to, string subject, string body)
        {
            await _emailGenerator.SendEmailAsync(to, subject, body);
        }
    }
}
