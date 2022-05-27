

using MyApp.Models;
using MyApp.Services;

using SQLite;
using Acr.UserDialogs;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace MyApp.Repository
{
    class Repository : IRepository
    {

        private ISQLiteAsyncConnectionProvider ConnectionProvider;
        private SQLiteAsyncConnection Connection;

        public Repository(ISQLiteAsyncConnectionProvider connectionProvider)
        {
            ConnectionProvider = connectionProvider;
            Connection = ConnectionProvider.GetConnection();
            CreateTable<Documents>();
        }

        public async Task<List<T>> GetData<T>(string table) where T : class, new()
        {
            return await Connection.QueryAsync<T>("SELECT * FROM '" + table + "'");
        }

        public async Task<bool> Insert(Documents list)
        {
            for (int i = 0; i < 5; i++)
            {
                list.NumberOfPage = i;
                await Connection.InsertAsync(list);
            }
            return true;
        }

        public bool IsTableExist(string table)
        {
            try
            {
                List<Documents> tableInfo = Connection.QueryAsync<Documents>("SELECT * FROM '" + table + "'").Result;

                if (tableInfo.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        public async void CreateTable<T>() where T : class, new()
        {
            try { await Connection.CreateTableAsync<T>(); }
            catch { UserDialogs.Instance.Alert("Restart the application", "Error", "Ok"); }
        }
    }
}
