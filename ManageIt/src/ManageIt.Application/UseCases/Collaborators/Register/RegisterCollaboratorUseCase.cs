using AutoMapper;
using ManageIt.Communication.CollaboratorDTOs;
using ManageIt.Domain.Entities;
using ManageIt.Domain.Repositories;
using ManageIt.Domain.Repositories.Collaborators;

namespace ManageIt.Application.UseCases.Collaborators.Register
{
    public class RegisterCollaboratorUseCase : IRegisterCollaboratorUseCase
    {
        private readonly ICollaboratorWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterCollaboratorUseCase(ICollaboratorWriteOnlyRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CollaboratorDTO> Execute(CollaboratorDTO collaborator, Guid companyId)
        {
            var collaboratorMap = _mapper.Map<Collaborator>(collaborator);
            var collaboratorExamsMap = _mapper.Map<List<CollaboratorExam>>(collaborator.Exams);

            collaboratorMap.Exams = collaboratorExamsMap;
            collaboratorMap.CompanyId = companyId;

            await _repository.Add(collaboratorMap);
            await _unitOfWork.Commit();

            return _mapper.Map<CollaboratorDTO>(collaboratorMap);
        }
    }
}
