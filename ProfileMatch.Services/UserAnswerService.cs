using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using ProfileMatch.Contracts;

namespace ProfileMatch.Services
{
 public   class UserAnswerService
    {
        private readonly IRepositoryWrapper wrapper;
        private readonly IMapper mapper;

        public UserAnswerService(IRepositoryWrapper wrapper, IMapper mapper)
        {
            this.wrapper = wrapper;
            this.mapper = mapper;
        }
    }
}
