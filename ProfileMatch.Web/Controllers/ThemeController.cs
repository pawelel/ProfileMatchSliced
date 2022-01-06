using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;

namespace ProfileMatch.Web.Controllers
{
    public class ThemeController : Controller
    {
        public IActionResult Index()
        {
            if (Request.Cookies["theme"]!=null)
            {
                ViewBag.message = Request.Cookies["theme"];
            }
            else
            {

            }
            return View();
        }
        [HttpPost]
        public IActionResult SetTheme(string data)
        {
            var options = new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddYears(1) };
            Response.Cookies.Append("theme", data, options );
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
