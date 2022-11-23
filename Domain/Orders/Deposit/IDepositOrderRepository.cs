using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Orders.Common.Model;

namespace Domain.Orders.Deposit
{
    public interface IDepositOrderRepository
    {
        Task<int> CreateNewDepositOrder(DepositOrder order);

        Task<IEnumerable<DepositOrder>> QueryDepositOrder(QueryOrderCondition query);

        Task<DepositOrder> GetDepositOrder(QueryOrderCondition query);
    }
}