using FrankIS.MyReporter.MobileClient.Config;
using FrankIS.MyReporter.MobileClient.Data.Contracts;
using FrankIS.MyReporter.MobileClient.Models;
using SQLite;

namespace FrankIS.MyReporter.MobileClient.Data;

public abstract class SqliteRepositoryBase<T> : IRepository<T> where T : SqliteEntity, new()
{
    private protected SQLiteAsyncConnection? _database;

    private protected async Task Init()
    {
        if (_database is not null)
        {
            return;
        }

        _database = new SQLiteAsyncConnection(ConfigConstants.DatabasePath, ConfigConstants.Flags);
        _ = await _database.CreateTableAsync<T>();
    }

    public virtual async Task<List<T>> GetItemsAsync()
    {
        await Init();
        return await _database!.Table<T>().ToListAsync();
    }

    public virtual async Task<T> GetItemAsync(int id)
    {
        await Init();
        return await _database!.Table<T>().Where(i => i.Id == id).FirstOrDefaultAsync();
    }

    public virtual async Task<int> GetItemsCountAsync()
    {
        await Init();
        return await _database!.Table<T>().CountAsync();
    }

    public virtual async Task<int> SaveItemAsync(T item)
    {
        await Init();
        return item.Id != 0 ? await _database!.UpdateAsync(item) : await _database!.InsertAsync(item);
    }

    public virtual async Task<int> DeleteItemAsync(T item)
    {
        await Init();
        return await _database!.DeleteAsync(item);
    }

    public virtual async Task<List<T>> FilterAsync(Func<T, bool> filter)
    {
        await Init();
        List<T> currencies = await _database!.Table<T>().Where(item => filter(item)).ToListAsync();
        return currencies;
    }
}
