using Domain.Environment.Model.Enums;
using Domain.ExtensionMethod;

namespace Domain.Environment.Model
{
    public class CustomerSystemException : Exception
    {
        public SystemResponseCodeEnum ResponseCode { get; set; }

        public CustomerSystemException(SystemResponseCodeEnum errorCode) : base(errorCode.GetDescription())
        {
            this.ResponseCode = errorCode;
            base.Data.Add("ResponseCode", errorCode);
        }

        public CustomerSystemException(SystemResponseCodeEnum errorCode, Exception innerException) : base(errorCode.GetDescription(), innerException)
        {
            this.ResponseCode = errorCode;
            base.Data.Add("ResponseCode", errorCode);
        }

        public CustomerSystemException(string message) : base(message)
        {
        }
    }
}