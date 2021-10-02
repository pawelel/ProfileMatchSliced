
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;

namespace ProfileMatch.Repositories
{
    public class UserAnswerRepository : IUserAnswerRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;

        public UserAnswerRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public Task<UserAnswer> Create(UserAnswer answer)
        {
            throw new System.NotImplementedException();
        }

        public Task<UserAnswer> Delete(UserAnswer answer)
        {
            throw new System.NotImplementedException();
        }

        public Task<UserAnswer> FindById(string userId, int answerOptionId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<UserAnswer>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<UserAnswer> Update(UserAnswer answer)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<UserAnswer>> GetUserAnswersForQuestion(int questionId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<UserAnswer>> GetUserAnswersForAnswerOptionAndQuestion(int answerOptionId, int questionId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<UserAnswer>> GetUserAnswersForUser(string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}