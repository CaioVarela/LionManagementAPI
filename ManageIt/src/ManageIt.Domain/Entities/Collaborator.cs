using ManageIt.Domain.Entities.Enums;

namespace ManageIt.Domain.Entities
{
    public class Collaborator
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? CPF { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public PositionEnum Position { get; set; }
        public List<CollaboratorExam> Exams { get; set; } = [];
        public bool IsFitAtPg => GetIsFitAtPg();
        public string ExamStatus => SetExamStatus();

        private bool GetIsFitAtPg()
        {
            return true;
        }

        private string SetExamStatus()
        {
            if ((Exams.Any(e => e.IsExpiringSoon)) && (Exams.Any(e => e.IsExpired)))
            {
                return "Has Expired and Expiring Exams";
            }
            else if (Exams.Any(e => e.IsExpiringSoon))
            {
                return "Has Exams Expiring";
            }
            else if (Exams.Any(e => e.IsExpired))
            {
                return "Has Expired Exams";
            }
            else
            {
                return "Ok";
            }
        }
    }
}
