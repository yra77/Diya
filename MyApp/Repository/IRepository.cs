
using System.Threading.Tasks;
using System.Collections.Generic;

using MyApp.Models;


namespace MyApp.Repository
{
    public interface IRepository
    {
        Task<List<T>> GetData<T>(string table) where T : class, new();
        Task<bool> Insert(Documents list);
        bool IsTableExist(string table);
    }
}
