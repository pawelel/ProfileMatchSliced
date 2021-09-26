using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;

namespace ProfileMatch.Repositories
{
   public class AnswerOptionRepository : RepositoryBase<AnswerOption>, IAnswerOptionRepository
    {
        public AnswerOptionRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {

        }
    }
}
