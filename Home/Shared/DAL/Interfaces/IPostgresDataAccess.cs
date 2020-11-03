using System.Collections.Generic;
using System.Threading.Tasks;

namespace Home.Shared.DAL
{
    public interface IPostgresDataAccess
    {
        Task<List<T>> LoadData<T, U>(string sql, U parameters);
        Task<int> SaveData<T>(string sql, T parameters);
    }
}