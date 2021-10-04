using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;

namespace ProfileMatch.Repositories
{
    public class AnswerOptionRepository : IAnswerOptionRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;

        public AnswerOptionRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task<List<AnswerOption>> GetAnswerOptionsWithQuestions()
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            return await repositoryContext.AnswerOptions.Include(o => o.Question).AsNoTracking().ToListAsync();
        }

        public async Task<AnswerOption> Create(AnswerOption answerOption)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            var data = await repositoryContext.AnswerOptions.AddAsync(answerOption);
            await repositoryContext.SaveChangesAsync();
            return data.Entity;
        }

        public async Task<AnswerOption> Delete(AnswerOption answerOption)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            var data = repositoryContext.AnswerOptions.Remove(answerOption).Entity;
            await repositoryContext.SaveChangesAsync();
            return data;
        }

        public async Task<AnswerOption> FindById(int answerOptionId)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            return await repositoryContext.AnswerOptions.SingleOrDefaultAsync(o => o.Id == answerOptionId);
        }

        public async Task<List<AnswerOption>> GetAnswerOptionsForQuestion(int questionId)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            return await repositoryContext.AnswerOptions.Include(o=>o.Question).Where(o => o.QuestionId == questionId).AsNoTracking().ToListAsync();
        }

        public async Task<List<AnswerOption>> GetAll()
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            return await repositoryContext.AnswerOptions.ToListAsync();
        }

        public async Task Update(AnswerOption answerOption)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            repositoryContext.AnswerOptions.Update(answerOption);
            await repositoryContext.SaveChangesAsync();
        }
    }
}