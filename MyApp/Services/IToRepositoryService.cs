
using MyApp.Models;

using System.Collections.Generic;
using System.Threading.Tasks;


namespace MyApp.Services
{
    public interface IToRepositoryService
    {
        Task<bool> Insert(Documents list);
        Task<List<T>> GetData<T>(string table) where T : class, new();
        bool IsTableExist(string table);
    }
}
