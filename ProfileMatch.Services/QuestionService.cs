using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using ProfileMatch.Contracts;
using ProfileMatch.Models.Models;
using ProfileMatch.Models.ViewModels;

namespace ProfileMatch.Services
{
    class QuestionService
    {
        private readonly IRepositoryWrapper wrapper;
        private readonly IMapper mapper;

        public QuestionService(IRepositoryWrapper wrapper, IMapper mapper)
        {
            this.wrapper = wrapper;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<QuestionVM>> FindAllAsync()
        {
            var request = await wrapper.Question.FindAllAsync();
            return mapper.Map<IEnumerable<QuestionVM>>(request);

        }

        public async Task Create(QuestionVM questionVM)
        {
            var doesExist = await wrapper.Question.FindSingleByConditionAsync(q => q.Name.Contains(questionVM.Name));
           var request = mapper.Map<Question>(questionVM);
            wrapper.Question.Create(request);
        }

    }
}
var doesExist = await wrapper.User.FindSingleByConditionAsync(u => u.NormalizedEmail.Equals(user.Email.ToUpper()));
if (doesExist == null)
{
    var userResult = mapper.Map<ApplicationUser>(user);
    wrapper.User.Create(userResult);
}