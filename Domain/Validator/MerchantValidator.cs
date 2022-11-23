using Domain.Environment.Model;
using Domain.Environment.Model.Enums;
using Domain.ExtensionMethod;

namespace Domain.Validator
{
    public static class MerchantValidator
    {
        public static void Valid(string merchantNo)
        {
            if (merchantNo.IsEmpty())
            {
                throw new CustomerSystemException(SystemResponseCodeEnum.MerchantNoNotFound);
            }

            if (!merchantNo.IsNumberAndEnglish() ||
                !merchantNo.StartsWith("GBP") ||
                 merchantNo.Length != 9)
            {
                throw new CustomerSystemException(SystemResponseCodeEnum.MerchantFormatError);
            }
        }
    }
}