using ManageIt.Communication.CollaboratorDTOs;
using ManageIt.Communication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageIt.Application.UseCases.Collaborators.Get.GetCollaboratorByExpiredExams
{
    public interface IGetExpiredCollaboratorExamUseCase
    {
        Task<ResponseGetAllCollaboratorsWithExpiringSoonExams> Execute(Guid companyId);
    }
}
