using ManageIt.Domain.Entities;

namespace ManageIt.Communication.ProductDTOs
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Balance { get; set; }
        public int MinimumStock { get; set; }
        public string Status { get; set; } = string.Empty;
        public int OrderQuantity { get; set; }
        public ApprovalCertification? ApprovalCertification { get; set; }
    }
}
