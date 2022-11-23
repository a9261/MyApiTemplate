using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Domain.Merchant;
using Domain.Merchants;
using Infrastructure.Models;
using Infrastructure.Repository.Common;

namespace Infrastructure.Repository.Merchant
{
    public class MerchantRepository : IMerchantRepository
    {
        private readonly BaseQueryRepository _baseQueryRepository;
        private readonly DbPaofenContext _dbContext;

        public MerchantRepository(BaseQueryRepository baseQueryRepository, DbPaofenContext dbContext)
        {
            _baseQueryRepository = baseQueryRepository;
            _dbContext = dbContext;
        }

        public Task<Domain.Merchants.Merchant> GetMerchantAsync(int merchantId)
        {
            var query = "SELECT * FROM Merchant WHERE Id = @MerchantId";
            var parameters = new DynamicParameters();
            parameters.Add("@MerchantId", merchantId);
            return _baseQueryRepository.GetAsync<Domain.Merchants.Merchant>(query, parameters);
        }

        public Task<Domain.Merchants.Merchant> GetMerchantAsync(string merchantNo)
        {
            var query = "SELECT * FROM Merchant WHERE MerchantNo = @MerchantNo";
            var parameters = new DynamicParameters();
            parameters.Add("@MerchantNo", merchantNo);
            return _baseQueryRepository.GetAsync<Domain.Merchants.Merchant>(query, parameters);
        }
    }
}