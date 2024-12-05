using ManageIt.Communication.CollaboratorDTOs;

namespace ManageIt.Application.UseCases.Collaborators.Get.GetUpcomingExpiringExams
{
    public interface IGetUpcomingExpiringExamsUseCase
    {
        Task<List<ExpiringExamDTO>> Execute();
    }
}
