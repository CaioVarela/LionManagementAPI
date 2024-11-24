using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageIt.Domain.Entities
{
    public class CollaboratorExam
    {
        public Guid Id { get; set; }
        public string ExamName { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; private set; }

        private DateTime _examDate;
        public DateTime ExamDate
        {
            get => _examDate;
            set
            {
                _examDate = value;
                SetExpiryDate();
            }
        }
        public Guid CollaboratorId { get; set; }
        public Collaborator Collaborator { get; set; } = default!;
        public bool IsExpiringSoon => ExpiryDate <= DateTime.Now.AddMonths(1) && ExpiryDate > DateTime.Now;
        public bool IsExpired => ExpiryDate <= DateTime.Now;

        public void SetExpiryDate()
        {
            ExpiryDate = CalculateExpiryDate();
        }

        private DateTime CalculateExpiryDate()
        {
            DateTime expiryDate;

            switch (ExamName.ToLower())
            {
                case "aso":
                    expiryDate = ExamDate.AddYears(1);
                    return expiryDate;

                case "nr10":
                case "nr35":
                case "direcao defensiva":
                case "har":
                    expiryDate = ExamDate.AddYears(2);
                    return expiryDate;
                default:
                    return new DateTime();
            }
        }
    }
}
