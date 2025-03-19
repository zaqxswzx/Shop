using Shop.Models;

namespace Shop.Dapper {
    public interface IAccountRepository {
        Task<IEnumerable<User>> GetAccounts();
    }
}