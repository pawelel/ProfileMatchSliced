using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace ProfileMatch.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CultureController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Set(string culture, string redirectUri)
        {
            if (culture != null)
            {
                HttpContext.Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(
                        new RequestCulture(culture, culture)));
            }
            if (redirectUri==null)
            {
                return LocalRedirect("/");
            }
            return LocalRedirect(redirectUri);
        }
    }
}
