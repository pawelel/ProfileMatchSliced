using Microsoft.Extensions.DependencyInjection;

using ProfileMatch.Contracts;
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
            services.AddTransient<DataManager<Question, ApplicationDbContext>>();
            services.AddTransient<DataManager<AnswerOption, ApplicationDbContext>>();
            services.AddTransient<DataManager<UserAnswer, ApplicationDbContext>>();
            services.AddTransient<DataManager<UserCategory, ApplicationDbContext>>();
            services.AddTransient<DataManager<Note, ApplicationDbContext>>();
            services.AddTransient<DataManager<UserNote, ApplicationDbContext>>();
            services.AddTransient<DataManager<ApplicationUser, ApplicationDbContext>>();
        }
    }
}