using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Restaurant.Client.Controllers
{
    [Route("[controller]/[action]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class CultureController : Controller
    {
        public IActionResult Set(string culture, string redirectUri)
        {
            if(culture != null)
            {
                var requestCulture = new RequestCulture(culture, culture);
                var cookieName = CookieRequestCultureProvider.DefaultCookieName;
                var cookieValue = CookieRequestCultureProvider.MakeCookieValue(requestCulture);

                HttpContext.Response.Cookies.Append(cookieName, cookieValue);
            }
            return LocalRedirect(redirectUri);
        }
    }
}
