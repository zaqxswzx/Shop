
using System.Data;

namespace Shop.Dapper {
    public interface IBaseRepository {
        public Task<IEnumerable<T>> QueryList<T>(Func<IDbConnection, Task<IEnumerable<T>>> exec);
    }
}