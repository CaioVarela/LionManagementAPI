using ManageIt.Application.AutoMapper;
using ManageIt.Application.UseCases.Collaborators.Delete;
using ManageIt.Application.UseCases.Collaborators.Delete.DeleteAll;
using ManageIt.Application.UseCases.Collaborators.Get.GetAllCollaborators;
using ManageIt.Application.UseCases.Collaborators.Get.GetCollaboratorByExpiredExams;
using ManageIt.Application.UseCases.Collaborators.Get.GetCollaboratorByExpiringSoon;
using ManageIt.Application.UseCases.Collaborators.Get.GetCollaboratorById;
using ManageIt.Application.UseCases.Collaborators.Get.GetCollaboratorByName;
using ManageIt.Application.UseCases.Collaborators.Get.GetUpcomingExpiringExams;
using ManageIt.Application.UseCases.Collaborators.Register;
using ManageIt.Application.UseCases.Collaborators.Update;
using ManageIt.Application.UseCases.Companies.Get.GetAllCollaboratorsUseCase;
using ManageIt.Application.UseCases.Companies.Register;
using ManageIt.Application.UseCases.Excel.AddCollaboratorsBySheet;
using ManageIt.Application.UseCases.Excel.AddProductFromSheet;
using ManageIt.Application.UseCases.Login.DoLogin;
using ManageIt.Application.UseCases.Products.Delete;
using ManageIt.Application.UseCases.Products.Get.GetAllProducts;
using ManageIt.Application.UseCases.Products.Get.GetProductById;
using ManageIt.Application.UseCases.Products.Get.GetProductByName;
using ManageIt.Application.UseCases.Products.Register;
using ManageIt.Application.UseCases.Products.Update;
using ManageIt.Application.UseCases.Users.Register;
using Microsoft.Extensions.DependencyInjection;

namespace ManageIt.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services) 
        {
            AddAutoMapper(services);
            AddUseCases(services);
        }

        private static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapping));
        }
        
        private static void AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<IRegisterCollaboratorUseCase, RegisterCollaboratorUseCase>();
            services.AddScoped<IAddCollaboratorsBySheetUseCase, AddCollaboratorsBySheetUseCase>();
            services.AddScoped<IRegisterProductUseCase, RegisterProductUseCase>();
            services.AddScoped<IRegisterCompanyUseCase, RegisterCompanyUseCase>();
            services.AddScoped<IAddProductFromSheetUseCase, AddProductFromSheetUseCase>();
            services.AddScoped<IUpdateCollaboratorUseCase,  UpdateCollaboratorUseCase>();
            services.AddScoped<IUpdateProductUseCase, UpdateProductUseCase>();
            services.AddScoped<IDeleteCollaboratorUseCase,  DeleteCollaboratorUseCase>();
            services.AddScoped<IDeleteAllCollaboratorUseCase,  DeleteAllCollaboratorUseCase>();
            services.AddScoped<IDeleteProductUseCase, DeleteProductUseCase>();
            services.AddScoped<IDeleteAllProductUseCase, DeleteAllProductUseCase>();
            services.AddScoped<IGetAllCollaboratorsUseCase, GetAllCollaboratorsUseCase>();
            services.AddScoped<IGetAllCompaniesUseCase, GetAllCompaniesUseCase>();
            services.AddScoped<IGetAllProductsUseCase, GetAllProductsUseCase>();
            services.AddScoped<IGetCollaboratorByIdUseCase, GetCollaboratorByIdUseCase>();
            services.AddScoped<IGetUpcomingExpiringExamsUseCase, GetUpcomingExpiringExamsUseCase>();
            services.AddScoped<IGetProductByIdUseCase, GetProductByIdUseCase>();
            services.AddScoped<IGetExpiredCollaboratorExamUseCase, GetExpiredCollaboratorExamUseCase>();
            services.AddScoped<IGetExpiringSoonCollaboratorExamUseCase, GetExpiringSoonCollaboratorExamUseCase>();
            services.AddScoped<IGetCollaboratorByNameUseCase, GetCollaboratorByNameUseCase>();
            services.AddScoped<IGetProductByNameUseCase, GetProductByNameUseCase>();
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
            services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
        }
    }
}
