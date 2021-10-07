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
        public async Task<UserAnswer> GetUserAnswer(string userId, int questionId)
        {//has user userAnswer this answerOption on this question
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            return await repositoryContext.UserAnswers
                .Include(u => u.AnswerOption.QuestionId == questionId).Where(a => a.ApplicationUserId == userId).AsNoTracking().FirstOrDefaultAsync();
            //return userAnswer where userId == userId and questionId == questionId
        }

        public async Task<UserAnswer> FindByIdAsync(string userId, int optionId)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            return await repositoryContext.UserAnswers.FindAsync(userId, optionId);
        }
        public UserAnswer FindById(string userId, int optionId)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            return repositoryContext.UserAnswers.Find(userId, optionId);
        }
        public int ShowLevel(Question question, string UserId)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            var answers = repositoryContext.UserAnswers;
            var options = repositoryContext.AnswerOptions;
            var data = (from o in options
                    where o.QuestionId == question.Id
                    join a in answers
                    on o.Id equals a.AnswerOptionId
                    where a.ApplicationUserId == UserId
                    select o.Level).FirstOrDefault();
            if (data>0)
            {
                return data;
            }
            return 0;
        }
        
    }
}