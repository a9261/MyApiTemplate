namespace Domain.Merchants
{
    public interface IMerchantRepository
    {
        Task<Merchants.Merchant> GetMerchantAsync(int merchantId);

        Task<Merchants.Merchant> GetMerchantAsync(string merchantNo);
    }
}