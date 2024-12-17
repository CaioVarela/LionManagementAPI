using ManageIt.Domain.Entities.Enums;

namespace ManageIt.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public Guid CompanyId { get; set; }
        public Company Company { get; set; } = null!;
        public string Role { get; set; } = Roles.TEAM_MEMBER;
    }
}
