using ManageIt.Domain.Entities;
using ManageIt.Domain.Entities.Enums;

namespace ManageIt.Communication.CompanyDTOs
{
    public class CompanyDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CNPJ { get; set; } = string.Empty;
        public CompanyType CompanyType { get; set; }
        public List<Guid> CollaboratorIds { get; set; } = [];
        public List<Guid> ProductIds { get; set; } = [];
        public List<Guid> UserIds { get; set; } = [];
    }
}
