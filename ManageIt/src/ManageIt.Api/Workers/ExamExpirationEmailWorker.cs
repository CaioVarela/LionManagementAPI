using ManageIt.Application.UseCases.Collaborators.Get.GetCollaboratorByExpiringSoon;
using ManageIt.Communication.CollaboratorDTOs;
using ManageIt.Domain.Email;
using ManageIt.Domain.Repositories.User;
using System.Text;


namespace ManageIt.Api.Workers
{
public class ExamExpirationEmailWorker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<ExamExpirationEmailWorker> _logger;

    public ExamExpirationEmailWorker(
        IServiceProvider serviceProvider,
        ILogger<ExamExpirationEmailWorker> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                    var now = DateTime.Now;
                    var nextRun = now.Date.AddHours(8);
                    if (now > nextRun)
                    {
                        nextRun = nextRun.AddDays(1);
                    }

                    var delay = nextRun - now;
                    await Task.Delay(delay, stoppingToken);

                    using var scope = _serviceProvider.CreateScope();
                var userRepository = scope.ServiceProvider.GetRequiredService<IUserReadOnlyRepository>();
                var expiringSoonUseCase = scope.ServiceProvider.GetRequiredService<IGetExpiringSoonCollaboratorExamUseCase>();
                var emailGenerator = scope.ServiceProvider.GetRequiredService<IEmailGenerator>();

                var qualityManager = await userRepository.GetQualityManager();
                if (qualityManager == null || string.IsNullOrEmpty(qualityManager.UserEmail))
                {
                    _logger.LogError("Gestor(a) da qualidade não encontrado(a) ou sem email cadastrado");
                    continue;
                }

                var collaborators = await expiringSoonUseCase.Execute(qualityManager.CompanyId);
                if (!collaborators.Collaborator.Any())
                {
                    _logger.LogInformation("Nenhum exame próximo ao vencimento encontrado");
                    continue;
                }

                var subject = "ManageIt - Relatório de Exames Próximos ao Vencimento";
                var body = BuildEmailBody(collaborators.Collaborator);

                await emailGenerator.SendEmailAsync(
                    qualityManager.UserEmail,
                    subject,
                    body
                );

                _logger.LogInformation("Relatório enviado para {email}", qualityManager.UserEmail);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar notificações de exames");
            }
        }
    }

    private string BuildEmailBody(List<CollaboratorDTO> collaborators)
    {
        var builder = new StringBuilder();
        builder.AppendLine("<h2>Relatório de Exames Próximos ao Vencimento</h2>");
        builder.AppendLine("<p>Seguem os colaboradores com exames próximos ao vencimento:</p>");

        foreach (var collaborator in collaborators)
        {
            builder.AppendLine($"<h3>Colaborador: {collaborator.Name}</h3>");
            builder.AppendLine("<ul>");

            foreach (var exam in collaborator.Exams.Where(e => e.IsExpiringSoon))
            {
                builder.AppendLine($"<li><strong>{exam.ExamName}</strong> - Vence em: {exam.ExpiryDate:dd/MM/yyyy}</li>");
            }

            builder.AppendLine("</ul>");
        }

        builder.AppendLine("<br>");
        builder.AppendLine("<p>Atenciosamente,<br>Sistema ManageIt</p>");

        return builder.ToString();
    }
}
}
