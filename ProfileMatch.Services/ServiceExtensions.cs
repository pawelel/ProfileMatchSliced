using Microsoft.Extensions.DependencyInjection;

using ProfileMatch.Contracts;
using ProfileMatch.Repositories;

namespace ProfileMatch.Services
{
    public static class ServiceExtensions
    {
        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IAnswerOptionRepository, AnswerOptionRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IUserAnswerRepository, UserAnswerRepository>();
            services.AddScoped<INoteRepository, NoteRepository>();
            services.AddScoped<IUserNeedCategoryRepository, UserNeedCategoryRepository>();
            services.AddScoped<IUserNoteRepository, UserNoteRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}