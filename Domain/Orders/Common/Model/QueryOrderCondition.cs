using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Orders.Common.Model
{
    public class QueryOrderCondition
    {
        public int? MerchantId { get; set; }

        public string? SystemOrderNo { get; set; }

        public string? MerchantOrderNo { get; set; }

        public DateTime? CreateStartTime { get; set; }
        public DateTime? CreateEndTime { get; set; }

        public DateTime? UpdateStartTime { get; set; }
        public DateTime? UpdateEndTime { get; set; }
    }
}