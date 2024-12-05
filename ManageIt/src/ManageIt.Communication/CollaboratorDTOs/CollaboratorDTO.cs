namespace ManageIt.Communication.CollaboratorDTOs
{
    public class CollaboratorDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty ;
        public string Position { get; set; } = string.Empty;
        public bool IsFitAtPg { get; set; }
        public string ExamStatus { get; set; } = string.Empty;
        public List<CollaboratorExamDTO> Exams { get; set; } = [];
    }
}
