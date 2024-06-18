using Restaurant.Client.Services.Interfaces;

namespace Restaurant.Client.Services
{

    public class CookiesService : ICookiesService
    {
        private readonly HttpContext _httpContext;

        public CookiesService(IHttpContextAccessor accessor)
        {
            _httpContext = accessor.HttpContext;
        }

        public string? GetCookie(string key)
        {
            return _httpContext.Request.Cookies[key];
        }

        public void DeleteCookie(string key)
        {
            _httpContext.Response.Cookies.Delete(key);
        }

        public void SetCookie(string key, string value)
        {
            _httpContext.Response.Cookies.Append(key, value);
        }
    }
}
