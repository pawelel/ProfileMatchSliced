using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using ProfileMatch.Models.ViewModels;

namespace ProfileMatch.Repositories
{
    public class UserAnswerRepository : IUserAnswerRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;

        public UserAnswerRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task<UserAnswer> Create(UserAnswer answer)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            var data = await repositoryContext.UserAnswers.AddAsync(answer);
            await repositoryContext.SaveChangesAsync();
            return data.Entity;
        }

        public async Task<UserAnswer> Delete(UserAnswer answer)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            var data = repositoryContext.UserAnswers.Remove(answer).Entity;
            await repositoryContext.SaveChangesAsync();
            return data;
        }

        public async Task<UserAnswer> FindById(UserAnswer userAnswer)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            if (userAnswer == null)
            {
            return new();
            }

            return await repositoryContext.UserAnswers.FindAsync(userAnswer.ApplicationUserId, userAnswer.AnswerOptionId);
        }

        public async Task<List<UserAnswer>> GetAll()
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            return await repositoryContext.UserAnswers.ToListAsync();
        }

        public async Task<UserAnswer> Update(UserAnswer answer)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            var existing = await repositoryContext.UserAnswers.FindAsync(answer.ApplicationUserId, answer.AnswerOptionId);
            if (existing != null)
            {
                repositoryContext.Entry(existing).CurrentValues.SetValues(answer);
                await repositoryContext.SaveChangesAsync();
                return existing;
            }
            else
            {
                return answer;
            }
        }

        public async Task<List<UserAnswer>> GetUserAnswersForQuestion(int questionId)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            return await repositoryContext.UserAnswers.Include(u => u.AnswerOption.Question).Where(o => o.AnswerOption.QuestionId == questionId).AsNoTracking().ToListAsync();
        }

        public async Task<List<UserAnswer>> GetUserAnswersForLevelAndQuestion(int level, int questionId)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            return await repositoryContext.UserAnswers.Include(u => u.AnswerOption.Question).Where(o => o.AnswerOption.Level == level).Where(o => o.AnswerOption.QuestionId == questionId).AsNoTracking().ToListAsync();
        }

        public async Task<List<UserAnswer>> GetUserAnswersForUser(string userId)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            return await repositoryContext.UserAnswers.Where(u => u.ApplicationUserId == userId).Include(a => a.AnswerOption).AsNoTracking().ToListAsync();
        }

        public async Task<int> GetUserAnswerLevel(string userId, int optionId)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            var data = await repositoryContext.UserAnswers.Where(u => u.ApplicationUserId == userId).Include(u => u.AnswerOption).Where(u => u.AnswerOptionId == optionId).SingleOrDefaultAsync();
                return data.AnswerOption.Level;
        }
    }
}