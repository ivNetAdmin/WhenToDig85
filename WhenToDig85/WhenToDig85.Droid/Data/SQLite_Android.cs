using SQLite.Net;
using SQLite.Net.Async;
using System;
using System.IO;
using WhenToDig90.Data;
using WhenToDig90.Droid.Data;

[assembly: Xamarin.Forms.Dependency(typeof(SQLite_Android))]
namespace WhenToDig85.Droid.Data
{
    public class SQLite_Android : ISQLite
    {
        private SQLiteConnectionWithLock _conn;

        public SQLite_Android()
        {
        }

        private string GetDatabasePath()
        {
            var sqliteFilename = "Wtd.db3";
            string documentsPath = Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, sqliteFilename);
            return path;
        }

        public SQLiteAsyncConnection GetAsyncConnection()
        {
            var dbpath = GetDatabasePath();

            var platForm = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();

            var connectionFactory = new Func<SQLiteConnectionWithLock>(
                () =>
                {
                    if (_conn == null)
                    {
                        _conn =
                            new SQLiteConnectionWithLock(platForm,
                                new SQLiteConnectionString(dbpath, storeDateTimeAsTicks: false));
                    }
                    return _conn;
                });

            return new SQLiteAsyncConnection(connectionFactory);
        }
    }
}
