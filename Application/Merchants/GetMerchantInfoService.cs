using Domain.Merchant;
using Domain.Merchants;

namespace Application.Merchants
{
    public class GetMerchantInfoService
    {
        private readonly IMerchantRepository _merchantRepository;

        public GetMerchantInfoService(IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository;
        }

        public async Task<Merchant> GetMerchantAsync(int merchantId)
        {
            return await _merchantRepository.GetMerchantAsync(merchantId);
        }

        public async Task<Merchant> GetMerchantAsync(string merchantNo)
        {
            return await _merchantRepository.GetMerchantAsync(merchantNo);
        }
    }
}