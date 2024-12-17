using AutoMapper;
using ManageIt.Communication.CollaboratorDTOs;
using ManageIt.Communication.CompanyDTOs;
using ManageIt.Communication.ProductDTOs;
using ManageIt.Communication.Requests;
using ManageIt.Communication.Responses;
using ManageIt.Domain.Entities;

namespace ManageIt.Application.AutoMapper
{
    internal class AutoMapping : Profile
    {
        public AutoMapping()
        {
            RequestToEntity();
            EntityToResponse();
        }

        private void RequestToEntity()
        {
            CreateMap<CollaboratorDTO, Collaborator>();
            CreateMap<ProductDTO, Product>().ForMember(dest => dest.Id, config => config.Ignore());
            CreateMap<CollaboratorExamDTO, CollaboratorExam>();
            CreateMap<CompanyDTO, Company>()
                .ForMember(dest => dest.Collaborators, opt => opt.Ignore())
                .ForMember(dest => dest.Products, opt => opt.Ignore())
                .ForMember(dest => dest.Users, opt => opt.Ignore());
            CreateMap<RequestRegisterUserJson, User>()
                .ForMember(dest => dest.PasswordHash, config => config.Ignore());
        }

        private void EntityToResponse()
        {
            CreateMap<Collaborator, CollaboratorDTO>();
            CreateMap<Product, ProductDTO>();
            CreateMap<CollaboratorExam, CollaboratorExamDTO>();
            CreateMap<Company, CompanyDTO>()
                .ForMember(dest => dest.CollaboratorIds, opt => opt.MapFrom(src =>
                    src.Collaborators.Where(c => c != null).Select(c => c.Id)))
                .ForMember(dest => dest.ProductIds, opt => opt.MapFrom(src =>
                    src.Products.Where(p => p != null).Select(p => p.Id)))
                .ForMember(dest => dest.UserIds, opt => opt.MapFrom(src =>
                    src.Users.Select(u => u.Id)));
            CreateMap<Collaborator, ResponseCollaboratorsDTO>();
            CreateMap<User, ResponseRegisteredUserJson>();
        }
    }
}
