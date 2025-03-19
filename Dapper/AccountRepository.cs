using Dapper;
using Shop.Models;
using System.Data;
using System.Data.Common;

namespace Shop.Dapper {
    public class AccountRepository(IDbConnection dbConnection) : BaseRepository(dbConnection), IAccountRepository {
        public async Task<IEnumerable<User>> GetAccounts() {
            return await QueryList(async conn => {
                return await conn.QueryAsync<User>("SELECT * FROM Users");
            });
        }
    }
}
