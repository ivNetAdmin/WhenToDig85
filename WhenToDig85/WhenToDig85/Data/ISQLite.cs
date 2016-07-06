using SQLite.Net.Async;

namespace WhenToDig85.Data
{
    public interface ISQLite
    {
        SQLiteAsyncConnection GetAsyncConnection();
    }
}
