using Dapper;
using System;
using System.Data;

namespace Shop.Dapper {
    public class BaseRepository : IBaseRepository {
        private readonly IDbConnection _dbConnection;
        public BaseRepository(IDbConnection dbConnection) {
            _dbConnection = dbConnection;
        }
        public async Task<IEnumerable<T>> QueryList<T>(Func<IDbConnection, Task<IEnumerable<T>>> exec) {
            return await exec(_dbConnection);
        }
    }
}
