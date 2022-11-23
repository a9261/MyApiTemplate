using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Orders.Common.Model;
using Domain.Orders.Deposit;
using Domain.ValueObjects;

namespace Application.Orders.Deposit
{
    public class CreateDepositOrderService
    {
        private readonly IDepositOrderRepository _depositOrderRepository;

        public CreateDepositOrderService(
            IDepositOrderRepository depositOrderRepository
            )
        {
            _depositOrderRepository = depositOrderRepository;
        }

        public async Task<string> CreateDepositOrder(UserRequestOrderInfo requestOrderInfo)
        {
            var currentAccount = "334568";
            var newOrder = DepositOrder.CreateOrder(currentAccount, new Money(requestOrderInfo.Amount), requestOrderInfo);
            await _depositOrderRepository.CreateNewDepositOrder(newOrder);
            return newOrder.SystemOrderNo;
        }
    }
}