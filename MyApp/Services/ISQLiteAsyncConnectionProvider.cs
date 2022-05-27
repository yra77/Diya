
using SQLite;

namespace MyApp.Services
{
    public interface ISQLiteAsyncConnectionProvider
    {
        SQLiteAsyncConnection GetConnection();
    }
}
