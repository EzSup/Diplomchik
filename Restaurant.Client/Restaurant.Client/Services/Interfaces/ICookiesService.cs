namespace Restaurant.Client.Services.Interfaces
{
    public interface ICookiesService
    {
        string? GetCookie(string key);
        void SetCookie(string key, string value);
        void DeleteCookie(string key);
    }
}
