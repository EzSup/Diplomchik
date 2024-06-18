namespace Restaurant.Client.Services.Interfaces
{
    public interface ILocalStorageService
    {
        Task AddItem(string key, string value);
        Task RemoveItem(string key);
        Task<string> GetItem(string key);
    }
}
