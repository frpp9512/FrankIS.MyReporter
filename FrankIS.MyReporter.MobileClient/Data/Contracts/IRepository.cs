using FrankIS.MyReporter.MobileClient.Models;

namespace FrankIS.MyReporter.MobileClient.Data.Contracts;

public interface IRepository<T> where T : SqliteEntity
{
    Task<List<T>> GetItemsAsync();
    Task<T> GetItemAsync(int id);
    Task<int> GetItemsCountAsync();
    Task<int> SaveItemAsync(T item);
    Task<int> DeleteItemAsync(T item);
    Task<List<T>> FilterAsync(Func<T, bool> filter);
}
