using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Domain.Environment.Model;
using Domain.Environment.Model.Enums;

namespace Application.ThirdParty.Models
{
    public class ThirdPartyResponse
    {
        public string ResponseCode { get; set; }
        public string Message { get; set; }

        public string RedirectUrl { get; set; }
        public string QrCodeImageBase64Contnet { get; set; }
        public string ThirdPartyOrderNo { get; set; }

        public void Valid([CallerFilePath] string file = null,
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = null)
        {
            if (string.IsNullOrEmpty(RedirectUrl) && string.IsNullOrEmpty(QrCodeImageBase64Contnet))
            {
                throw new CustomerSystemException(SystemResponseCodeEnum.SystemError,
                    new Exception($"ThirdPartyResponse Error RedirectUrl or ImageBase64 is empty  On {file}, {line}, Caller: {member}"));
            }
        }
    }
}