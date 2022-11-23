using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Orders.Common.Model
{
    public class UserRequestOrderInfo
    {
        public int MerchantId { get; set; }
        public decimal Amount { get; set; }
        public string MerchantOrderNo { get; set; }
        public string MemberCode { get; set; }
        public string Ip { get; set; }
        public string ReturnUrl { get; set; }
        public DateTime OrderTime { get; set; }
    }
}