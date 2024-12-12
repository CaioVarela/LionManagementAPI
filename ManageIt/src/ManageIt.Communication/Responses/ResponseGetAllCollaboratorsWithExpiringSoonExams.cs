using ManageIt.Communication.CollaboratorDTOs;

namespace ManageIt.Communication.Responses
{
    public class ResponseGetAllCollaboratorsWithExpiringSoonExams
    {
        public List<CollaboratorDTO> Collaborator { get; set; } = [];
        public int CollaboratorsCount { get; set; }
        public float CollaboratorsPercentage { get; set; }
        public int AsoExpiring { get; set; } 
        public  int HarExpiring { get; set; } 
        public int Nr10Expiring { get; set; }
        public int Nr35Expiring { get; set; }
        public int AvaliacaoPsicologicaExpiring { get; set; }
        public int DirecaoDefensivaExpiring { get; set; }
        public int CnhExpiring { get; set; }
    }
}
