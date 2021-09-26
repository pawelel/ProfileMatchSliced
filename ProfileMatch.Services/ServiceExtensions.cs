using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using ProfileMatch.Contracts;
using ProfileMatch.Repositories;

namespace ProfileMatch.Services
{
    public static class ServiceExtensions
    {
        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }

        public static void ConfigureRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}
