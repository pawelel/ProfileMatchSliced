using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(ProfileMatch.Web.Areas.Identity.IdentityHostingStartup))]

namespace ProfileMatch.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}