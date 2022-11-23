using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Domain.ExtensionMethod;
using Domain.Orders.Common.Model;
using Domain.Orders.Deposit;
using Infrastructure.Extensions;
using Infrastructure.Models;
using Infrastructure.Repository.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infrastructure.Repository.Orders.Deposit
{
    public class DepositOrderRepository : IDepositOrderRepository
    {
        private readonly DbPaofenContext _dbContext;
        private readonly BaseQueryRepository _baseQueryRepository;

        public DepositOrderRepository(DbPaofenContext dbContext, BaseQueryRepository baseQueryRepository)
        {
            _dbContext = dbContext;
            _baseQueryRepository = baseQueryRepository;
        }

        public Task<int> CreateNewDepositOrder(DepositOrder order)
        {
            _dbContext.OrderDeposits.AddAsync(order);
            return _dbContext.SaveChangesAsync();
        }

        public Task<IEnumerable<DepositOrder>> QueryDepositOrder(QueryOrderCondition query)
        {
            var sql = @"SELECT
                            BankCode,SystemOrderNo
                        FROM dbo.OrderDeposit
                        WHERE 1=1
            ";
            var (finalSql, parameters) = GeneratorSimpleQuery(sql, query);

            return _baseQueryRepository.QueryAsync<DepositOrder>(sql, parameters);
        }

        public Task<DepositOrder> GetDepositOrder(QueryOrderCondition query)
        {
            var sql = @"SELECT
                            BankCode,SystemOrderNo
                        FROM dbo.OrderDeposit
                        WHERE 1=1
            ";
            var (finalSql, parameters) = GeneratorSimpleQuery(sql, query);

            return _baseQueryRepository.GetAsync<DepositOrder>(finalSql, parameters);
        }

        private (string, DynamicParameters dynamicParameters) GeneratorSimpleQuery(string sql, QueryOrderCondition query)
        {
            var parameters = new DynamicParameters();
            if (!string.IsNullOrEmpty(query.MerchantOrderNo))
            {
                sql += " AND MerchantOrderNo = @MerchantOrderNo ";
                parameters.Add("MerchantOrderNo", query.MerchantOrderNo.ToVarchar());
            }
            if (!string.IsNullOrEmpty(query.SystemOrderNo))
            {
                sql += " AND SystemOrderNo = @SystemOrderNo ";
                parameters.Add("SystemOrderNo", query.SystemOrderNo.ToVarchar());
            }
            if (query.MerchantId != null)
            {
                sql += " AND MerchantId = @MerchantId ";
                parameters.Add("MerchantId", query.MerchantId);
            }
            if (query.CreateStartTime != null && query.CreateEndTime != null)
            {
                sql += " AND (CreateTime BETWEEN @CreateStartTime AND @CreateEndTime) ";
                parameters.Add("CreateStartTime", query.CreateStartTime);
                parameters.Add("CreateEndTime", query.CreateEndTime);
            }
            if (query.UpdateStartTime != null && query.UpdateEndTime != null)
            {
                sql += " AND (UpdateTime BETWEEN @UpdateStartTime AND @UpdateEndTime) ";
                parameters.Add("UpdateStartTime", query.UpdateStartTime);
                parameters.Add("UpdateEndTime", query.UpdateEndTime);
            }

            return (sql, parameters);
        }
    }
}