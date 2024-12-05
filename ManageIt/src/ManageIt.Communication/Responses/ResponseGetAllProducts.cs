using ManageIt.Communication.ProductDTOs;

namespace ManageIt.Communication.Responses
{
    public class ResponseGetAllProducts
    {
        public List<ProductDTO> Product { get; set; } = [];
        public double StockOkPercentage { get; set; }
        public int EPICount { get; set; }
        public double EPIOkPercentage { get; set; }
        public double ApprovalCertificationExpiryDateNotOkCount { get; set; }
        public double ApprovalCertificationExpiryDateNotOkPercentage { get; set; }
    }
}
