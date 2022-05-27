
using MyApp.Services;

using SQLite;
using System.IO;


namespace MyApp.Droid.Services
{
    class SQLiteAsyncConnectionProvider : ISQLiteAsyncConnectionProvider
    {
        private SQLiteAsyncConnection Connection { get; set; }

        public SQLiteAsyncConnection GetConnection()
        {
            if (Connection != null)
            {
                return Connection;
            }

            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            path = Path.Combine(path, "database.db3");
            return Connection = new SQLiteAsyncConnection(path);
        }
    }
}