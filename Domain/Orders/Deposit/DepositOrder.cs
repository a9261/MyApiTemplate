using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Environment;
using Domain.Environment.Model;
using Domain.Environment.Model.Enums;
using Domain.ValueObjects;
using Domain.Orders.Common.Enums;
using Domain.Orders.Common.Model;

namespace Domain.Orders.Deposit
{
    public class DepositOrder
    {
        public int Id { get; set; }
        public string? BankCode { get; set; }

        public string? SystemOrderNo { get; set; }

        public int MerchantId { get; set; }

        public string? AccountNo { get; set; }

        public string? AccountName { get; set; }

        public string? MerchantOrderNo { get; set; }

        public string? MemberCode { get; set; }

        public string? Ip { get; set; }

        public decimal Amount { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime OrderTime { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public int CreateBy { get; set; }

        public int UpdateBy { get; set; }

        public DateTime? ExpiredTime { get; set; }

        public string? Remark { get; set; }

        public string? ReturnUrl { get; set; }

        public string? ExtraContent { get; set; }

        public static DepositOrder CreateOrder(string targetAccount, Money money, UserRequestOrderInfo requestOrderInfo)
        {
            if (!money.IsPositiveOrZero())
            {
                throw new CustomerSystemException("Amount must be positive");
            }
            return new DepositOrder()
            {
                MerchantOrderNo = requestOrderInfo.MerchantOrderNo,
                SystemOrderNo = GenerateOrderNo(),
                AccountNo = targetAccount,
                AccountName = targetAccount,
                BankCode = targetAccount,
                Amount = money.Amount,
                MemberCode = requestOrderInfo.MemberCode,
                Ip = requestOrderInfo.Ip,
                ReturnUrl = requestOrderInfo.ReturnUrl,
                OrderTime = requestOrderInfo.OrderTime,
                MerchantId = requestOrderInfo.MerchantId,
                ExpiredTime = DateTimeHelper.GetUTCNow().AddSeconds(9487),
                CreateBy = (int)UserType.System,
                UpdateBy = (int)UserType.System,
                CreateTime = DateTimeHelper.GetUTCNow(),
                UpdateTime = DateTimeHelper.GetUTCNow(),
                Status = OrderStatus.Init
            };
        }

        private static string GenerateOrderNo()
        {
            string str = DateTime.UtcNow.ToString("yyyyMMddhhmmss");
            int num = new System.Random(Guid.NewGuid().GetHashCode()).Next(10000000, 99999999);
            return string.Format("D{0}000{1}", (object)str, (object)num);
        }
    }
}