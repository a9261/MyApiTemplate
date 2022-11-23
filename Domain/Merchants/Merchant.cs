using Domain.Merchants.Models.Enums;

namespace Domain.Merchants
{
    public class Merchant
    {
        public int Id { get; set; }
        public string MerchantNo { get; set; }
        public string MerchantName { get; set; }

        public MerchantStatus Status { get; set; }
        public string DepositNotifyUrl { get; set; }
        public string PayoutNotifyUrl { get; set; }
        public string SignKey { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}