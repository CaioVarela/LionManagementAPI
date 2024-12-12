using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageIt.Communication.CollaboratorDTOs
{
    public class ExpiringExamDTO
    {
        public string Date { get; set; } = string.Empty;
        public int ExpiringCount { get; set; }
    }
}
