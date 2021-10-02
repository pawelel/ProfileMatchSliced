using System.Collections.Generic;
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

        public Task<List<AnswerOption>> GetAnswerOptionsWithQuestions()
        {
            throw new System.NotImplementedException();
        }

        public Task<AnswerOption> Create(AnswerOption answerOption)
        {
            throw new System.NotImplementedException();
        }

        public Task<AnswerOption> Delete(AnswerOption answerOption)
        {
            throw new System.NotImplementedException();
        }

        public Task<AnswerOption> FindByName(string answerOptionName)
        {
            throw new System.NotImplementedException();
        }

        public Task<AnswerOption> FindById(int answerOptionId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<AnswerOption>> GetAnswerOptionsForQuestion(int questionId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<AnswerOption>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<AnswerOption> Update(AnswerOption answerOption)
        {
            throw new System.NotImplementedException();
        }
    }
}