namespace ApiLayer.Models
{
    public class CustomerApiRequest
    {
        public string MerchantNo { get; set; }
        public string SignMsg { get; set; }
        public string Request { get; set; }
    }
}