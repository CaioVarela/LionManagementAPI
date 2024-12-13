using ManageIt.Domain.Email;
using ManageIt.Domain.Repositories;
using ManageIt.Domain.Repositories.Collaborators;
using ManageIt.Domain.Repositories.Companies;
using ManageIt.Domain.Repositories.Products;
using ManageIt.Domain.Repositories.User;
using ManageIt.Domain.Security.Cryptography;
using ManageIt.Domain.Security.Tokens;
using ManageIt.Infrastructure.DataAccess;
using ManageIt.Infrastructure.DataAccess.Repositories;
using ManageIt.Infrastructure.Email;
using ManageIt.Infrastructure.Security.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ManageIt.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddDbContext(services, configuration);
            AddToken(services, configuration);
            AddRepositories(services);
            AddEmail(services, configuration);
            services.AddScoped<IPasswordEncripter, Security.Cryptography.BCrypt>();
        }

        private static void AddToken(IServiceCollection services, IConfiguration configuration)
        {
            var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpiresMinutes");
            var signingKey = configuration.GetValue<string>("Settings:Jwt:SigninKey");

            services.AddScoped<IAccessTokenGenerator>(config => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
        }

        private static void AddEmail(this IServiceCollection services, IConfiguration configuration)
        {
            var smtpServer = configuration.GetValue<string>("Settings:EmailSettings:SmtpServer");
            var smtpPort = configuration.GetValue<int>("Settings:EmailSettings:SmtpPort");
            var smtpUsername = configuration.GetValue<string>("Settings:EmailSettings:SmtpUsername");
            var smtpPassword = configuration.GetValue<string>("Settings:EmailSettings:SmtpPassword");

            services.AddScoped<IEmailGenerator>(config => new EmailGenerator(smtpServer!, smtpPort, smtpUsername!, smtpPassword!));
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICollaboratorWriteOnlyRepository, CollaboratorsRepository>();
            services.AddScoped<IProductWriteOnlyRepository, ProductsRepository>();
            services.AddScoped<ICompanyWriteOnlyRepository, CompaniesRepository>();
            services.AddScoped<ICollaboratorReadOnlyRepository, CollaboratorsRepository>();
            services.AddScoped<ICompanyReadOnlyRepository, CompaniesRepository>();
            services.AddScoped<IProductReadOnlyRepository, ProductsRepository>();
            services.AddScoped<ICollaboratorUpdateOnlyRepository, CollaboratorsRepository>();
            services.AddScoped<ICompanyUpdateOnlyRepository, CompaniesRepository>();
            services.AddScoped<IProductUpdateOnlyRepository, ProductsRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        }

        private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Connection");
            var version = new Version(8, 4, 0);
            var serverVersion = new MySqlServerVersion(version);

            services.AddDbContext<CollaboratorDbContext>(config => config.UseMySql(connectionString, serverVersion));
        }
    }
}
