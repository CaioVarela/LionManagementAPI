namespace ManageIt.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Balance { get; set; }
        public int MinimumStock { get; set; }
        public bool IsEPI => HasApprovalCertification();
        public string Status => GetProductStatus();
        public int OrderQuantity => GetOrderQuantity();
        public ApprovalCertification? ApprovalCertification { get; set; }

        public string GetProductStatus()
        {
            var balance = Balance;
            var minimumStock = MinimumStock;
            if (balance - minimumStock < 0)
            {
                return "Estoque Abaixo do Limite Minimo";
            }
            else
            {
                return "Estoque Adequado";
            }
        }

        public int GetOrderQuantity()
        {
            var necessaryOrder = MinimumStock - Balance;

            if (necessaryOrder <= 0)
            {
                return 0;
            }
            else
            {
                return Math.Abs(necessaryOrder);
            }
        }

        public bool HasApprovalCertification()
        {
            if (ApprovalCertification is not null)
                return true;
            else
                return false;
        }
    }
}
