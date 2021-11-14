using Microsoft.EntityFrameworkCore;

using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using ProfileMatch.Models.ViewModels;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileMatch.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;

        public UserRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task<ApplicationUser> Create(ApplicationUser user)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            var data = await repositoryContext.AddAsync(user);
            await repositoryContext.SaveChangesAsync();
            return data.Entity;
        }

        public async Task<ApplicationUser> Delete(ApplicationUser user)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            var data = repositoryContext.Users.Remove(user).Entity;
            await repositoryContext.SaveChangesAsync();
            return data;
        }

        public async Task<ApplicationUser> FindByEmail(string email)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            return await repositoryContext.Users.FirstOrDefaultAsync(u => u.Email.ToUpper().Equals(email.ToUpper()));
        }

        public async Task<ApplicationUser> FindById(string id)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            return await repositoryContext.Users.FirstOrDefaultAsync(u => u.Id.Equals(id));
        }

        public async Task<List<ApplicationUser>> GetAll()
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            return await repositoryContext.Users.AsNoTracking().ToListAsync();
        }

        public async Task<List<QuestionUserLevelVM>> GetUsersWithQuestionAnswerLevel()
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            var users = repositoryContext.Users;
            var questions = repositoryContext.Questions;
            var categories = repositoryContext.Categories;
            var answers = repositoryContext.UserAnswers;
            var options = repositoryContext.AnswerOptions;
            return await
                (from u in users
                 join a in answers
                 on u.Id equals a.ApplicationUserId
                 join o in options
                 on a.AnswerOptionId equals o.Id
                 join q in questions
                 on o.QuestionId equals q.Id
                 join c in categories
                 on q.CategoryId equals c.Id
                 select new QuestionUserLevelVM
                 {
                     QuestionId = q.Id,
                     QuestionName = q.Name,
                     UserId = u.Id,
                     FirstName = u.FirstName,
                     LastName = u.LastName,
                     Level = o.Level,
                     CategoryId = c.Id,
                     CategoryName = c.Name
                 }).ToListAsync();
        }

        public async Task<List<QuestionUserLevelVM>> GetUsersWithQuestionAnswerLevel(int questionId, int level)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            var users = repositoryContext.Users;
            var questions = repositoryContext.Questions;
            var answers = repositoryContext.UserAnswers;
            var options = repositoryContext.AnswerOptions;
            return await
                (from u in users
                 join a in answers
                 on u.Id equals a.ApplicationUserId
                 join o in options
                 on a.AnswerOptionId equals o.Id
                 join q in questions
                 on o.QuestionId equals q.Id
                 where q.Id == questionId && o.Level == level
                 select new QuestionUserLevelVM
                 {
                     QuestionId = q.Id,
                     QuestionName = q.Name,
                     UserId = u.Id,
                     FirstName = u.FirstName,
                     LastName = u.LastName,
                     Level = o.Level
                 }).ToListAsync();
        }

        public async Task<ApplicationUser> Update(ApplicationUser user)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            var existing = await repositoryContext.Users.FindAsync(user.Id);
            repositoryContext.Entry(existing).CurrentValues.SetValues(user);
            await repositoryContext.SaveChangesAsync();
            return existing;
        }
    }
}