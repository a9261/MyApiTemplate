using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repository.Common
{
    public class BaseQueryRepository
    {
        private readonly string _connectionString;

        public BaseQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:TargetDb"];
        }

        public Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null,
            [CallerFilePath] string file = null,
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = null)
        {
            try
            {
                var connection = new SqlConnection(_connectionString);
                return connection.QueryAsync<T>(sql, param);
            }
            catch (Exception e)
            {
                throw new Exception($"{e.Message}  On {file}, {line}, Caller: {member}");
            }
        }

        public Task<T> GetAsync<T>(string sql, object param = null,
            [CallerFilePath] string file = null,
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = null)
        {
            try
            {
                var connection = new SqlConnection(_connectionString);
                return connection.QueryFirstOrDefaultAsync<T>(sql, param);
            }
            catch (Exception e)
            {
                throw new Exception($"{e.Message}  On {file}, {line}, Caller:{member}");
            }
        }

        public List<T> Query<T>(string sql, object param = null,
            [CallerFilePath] string file = null,
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = null)
        {
            try
            {
                var connection = new SqlConnection(_connectionString);
                return connection.Query<T>(sql, param).ToList();
            }
            catch (Exception e)
            {
                throw new Exception($"{e.Message}  On {file}, {line}, Caller: {member}");
            }
        }

        public T Get<T>(string sql, object param = null,
            [CallerFilePath] string file = null,
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = null)
        {
            try
            {
                var connection = new SqlConnection(_connectionString);
                return connection.QueryFirstOrDefault<T>(sql, param);
            }
            catch (Exception e)
            {
                throw new Exception($"{e.Message}  On {file}, {line}, Caller: {member}");
            }
        }
    }
}