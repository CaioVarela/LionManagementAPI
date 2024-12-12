using AutoMapper;
using ManageIt.Communication.CollaboratorDTOs;
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
            CreateMap<RequestRegisterUserJson, User>()
                .ForMember(dest => dest.PasswordHash, config => config.Ignore());
        }

        private void EntityToResponse()
        {
            CreateMap<Collaborator, CollaboratorDTO>();
            CreateMap<Product, ProductDTO>();
            CreateMap<CollaboratorExam, CollaboratorExamDTO>();
            CreateMap<Collaborator, ResponseCollaboratorsDTO>();
            CreateMap<User, ResponseRegisteredUserJson>();
        }
    }
}
