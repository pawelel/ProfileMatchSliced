
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using ProfileMatch.Models.Responses;

namespace ProfileMatch.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;

        public QuestionRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task<List<Question>> GetQuestionsWithCategories()
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            return await repositoryContext.Questions.Include(c => c.Category).AsNoTracking().ToListAsync();
        

        }

        public Task<Question> Create(Question question)
        {
            throw new NotImplementedException();
        }

        public Task<Question> Delete(Question question)
        {
            throw new NotImplementedException();
        }

        public Task<Question> FindByName(string questionName)
        {
            throw new NotImplementedException();
        }

        public Task<Question> FindById(int questionId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Question>> GetQuestionsForCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Question>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Question> Update(Question question)
        {
            throw new NotImplementedException();
        }

        public Task<List<Question>> GetQuestionsWithCondition(Expression<Func<Question, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
