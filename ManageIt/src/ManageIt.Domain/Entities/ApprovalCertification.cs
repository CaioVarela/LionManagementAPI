namespace ManageIt.Domain.Entities
{
    public class ApprovalCertification
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string? Manufacturer { get; set; }
        public int CertificationNumber { get; set; }
        public DateTime CertificationExpiryDate { get; set; }
    }
}
