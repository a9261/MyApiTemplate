using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Orders.Common.Model;
using Domain.Orders.Deposit;

namespace Application.Orders.Deposit
{
    public class QueryDepositOrderService
    {
        private readonly IDepositOrderRepository _depositOrderRepository;

        public QueryDepositOrderService(IDepositOrderRepository depositOrderRepository)
        {
            _depositOrderRepository = depositOrderRepository;
        }

        public Task<DepositOrder> GetDepositOrder(QueryOrderCondition condition)
        {
            return _depositOrderRepository.GetDepositOrder(condition);
        }
    }
}