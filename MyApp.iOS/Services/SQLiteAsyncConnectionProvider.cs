using Foundation;
using MyApp.Models.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UIKit;

namespace MyApp.iOS.Services
{
    class SQLiteAsyncConnectionProvider : ISQLiteAsyncConnectionProvider
    {
        private SQLiteAsyncConnection Connection { get; set; }

        public SQLiteAsyncConnection GetConnection()
        {
            if (Connection != null) { return Connection; }

            var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            path = Path.Combine(path, "..", "Library", "database.db3");
            return Connection = new SQLiteAsyncConnection(path);
        }
    }
}