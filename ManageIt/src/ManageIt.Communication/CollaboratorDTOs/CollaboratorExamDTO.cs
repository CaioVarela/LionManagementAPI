using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageIt.Communication.CollaboratorDTOs
{
    public class CollaboratorExamDTO
    {
        public string ExamName { get; set; } = string.Empty;
        public DateTime? ExpiryDate { get; set; }
        public DateTime ExamDate { get; set; }
        public bool IsExpiringSoon { get; set; }
        public bool IsExpired { get; set; }
    }
}
