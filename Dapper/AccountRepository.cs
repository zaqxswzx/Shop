using Dapper;
using Shop.Models;
using System.Data;
using System.Data.Common;

namespace Shop.Dapper {
    public class AccountRepository(IDbConnection dbConnection) : BaseRepository(dbConnection), IAccountRepository {
        public async Task<IEnumerable<Member>> GetAccounts() {
            return await QueryList(async conn => {
                return await conn.QueryAsync<Member>("SELECT * FROM Users");
            });
        }
    }
}
