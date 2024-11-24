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

        private bool GetIsFitAtPg()
        {
            switch(Position)
            {
                case PositionEnum.ApprenticeAdministrativeAssistant:
                case PositionEnum.AdministrativeAssistant:
                case PositionEnum.Chef:
                case PositionEnum.Watchman:
                case PositionEnum.Doorman:
                    return true;
                default:
                    return false;
            }
        }
    }
}
