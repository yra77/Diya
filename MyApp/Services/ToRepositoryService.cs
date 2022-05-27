
using MyApp.Models;
using MyApp.Repository;

using System.Collections.Generic;
using System.Threading.Tasks;


namespace MyApp.Services
{

    class ToRepositoryService: IToRepositoryService
    {
        IRepository repository;
        public ToRepositoryService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<bool> Insert(Documents list)
        {
            return await repository.Insert(list);
        }
        public async Task<List<T>> GetData<T>(string table) where T : class, new()
        {
            return await repository.GetData<T>(table);
        }
        public bool IsTableExist(string table)
        {
            return repository.IsTableExist(table);
        }
    }
}
