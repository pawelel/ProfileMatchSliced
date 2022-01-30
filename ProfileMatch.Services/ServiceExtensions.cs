using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using ProfileMatch.Repositories;

namespace ProfileMatch.Services
{
    public static class ServiceExtensions
    {
        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        
            services.AddScoped<IRedirection, Redirection>();
        }
    }
}