using ManageIt.Domain.Entities.Enums;

namespace ManageIt.Domain.Entities
{
    public class Company
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CNPJ {  get; set; } = string.Empty;
        public CompanyType CompanyType { get; set; }
        public List<Collaborator?> Collaborators { get; set; } = [];
        public List<Product?> Products { get; set; } = [];
        public List<User?> Users { get; set; } = [];
    }
}