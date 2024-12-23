using AutoMapper;
using ManageIt.Communication.CollaboratorDTOs;
using ManageIt.Domain.Repositories.Collaborators;

namespace ManageIt.Application.UseCases.Collaborators.Get.GetAllCollaborators
{
    public class GetAllCollaboratorsUseCase : IGetAllCollaboratorsUseCase
    {
        private readonly ICollaboratorReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetAllCollaboratorsUseCase(ICollaboratorReadOnlyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CollaboratorDTO>> Execute(Guid companyId)
        {
            var result = await _repository.GetAll();

            var collaboratorsFromCompany = result.Where(c => c.CompanyId == companyId).ToList();

            var response = _mapper.Map<List<CollaboratorDTO>>(collaboratorsFromCompany);

            return response;
        }
    }
}
