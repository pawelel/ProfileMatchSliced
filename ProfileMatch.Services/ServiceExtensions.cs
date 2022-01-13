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
            services.AddTransient<DataManager<Department, ApplicationDbContext>>();
            services.AddTransient<DataManager<Category, ApplicationDbContext>>();
            services.AddTransient<DataManager<ClosedQuestion, ApplicationDbContext>>();
            services.AddTransient<DataManager<AnswerOption, ApplicationDbContext>>();
            services.AddTransient<DataManager<UserClosedAnswer, ApplicationDbContext>>();
            services.AddTransient<DataManager<UserCategory, ApplicationDbContext>>();
            services.AddTransient<DataManager<OpenQuestion, ApplicationDbContext>>();
            services.AddTransient<DataManager<UserOpenAnswer, ApplicationDbContext>>();
            services.AddTransient<DataManager<ApplicationUser, ApplicationDbContext>>();
            services.AddTransient<DataManager<IdentityUserRole<string>, ApplicationDbContext>>();
            services.AddTransient<DataManager<IdentityRole, ApplicationDbContext>>();
            services.AddTransient<DataManager<JobTitle, ApplicationDbContext>>();
            services.AddScoped<IRedirection, Redirection>();
        }
    }
}