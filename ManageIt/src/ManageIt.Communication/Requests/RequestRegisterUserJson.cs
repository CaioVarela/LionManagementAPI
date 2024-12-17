namespace ManageIt.Communication.Requests
{
    public class RequestRegisterUserJson
    {
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public Guid CompanyId { get; set; }
        public string Password { get; set; } = string.Empty;
        public string Role {  get; set; } = string.Empty;
    }
}
