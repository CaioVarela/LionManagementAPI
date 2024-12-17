using AutoMapper;
using ManageIt.Application.UseCases.Collaborators.Get.GetCollaboratorByName;
using ManageIt.Communication.CollaboratorDTOs;
using ManageIt.Domain.Repositories.Collaborators;

namespace ManageIt.Application.UseCases.Collaborators.Get.GetCollaboratorById
{
    public class GetCollaboratorByNameUseCase : IGetCollaboratorByNameUseCase
    {
        private readonly ICollaboratorReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetCollaboratorByNameUseCase(ICollaboratorReadOnlyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CollaboratorDTO>> Execute(string  name, Guid companyId)
        {
            var getByName = await _repository.GetByName(name);
            var result = getByName.CompanyId == companyId;

            if(result)
                return _mapper.Map<List<CollaboratorDTO>>(getByName);

            return [];
        }
    }
}
