namespace Domain.Merchant.Models
{
    public class MerchantRequest
    {
        /// <summary>
        /// 請求內容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 請求內容自行計算後的簽名
        /// </summary>
        public string MessageSign { get; set; }

        /// <summary>
        /// 商戶號
        /// </summary>
        public string MerchantNo { get; set; }

        /// <summary>
        /// 商戶簽名密鑰
        /// </summary>
        public string SignKey { get; set; }
    }
}