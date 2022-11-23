using Domain.Environment.Model;
using Domain.Environment.Model.Enums;
using Domain.ExtensionMethod;
using Domain.Merchant;
using Domain.Merchant.Models;

namespace Domain.Validator
{
    public static class MessageSignValidator
    {
        public static void Valid(MerchantRequest request)
        {
            string plainText = $"merchantNo:{request.MerchantNo}&request:{request.Content}&key:{request.SignKey}";
            string computedHash = plainText.ToMD5().ToUpper();
            if (request.MessageSign.ToUpper() != computedHash)
            {
                throw new CustomerSystemException(SystemResponseCodeEnum.SignVerifyFailed);
            }
        }
    }
}