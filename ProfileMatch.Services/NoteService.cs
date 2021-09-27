using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using ProfileMatch.Contracts;

namespace ProfileMatch.Services
{
   public class NoteService
    {
        private readonly IRepositoryWrapper wrapper;
        private readonly IMapper mapper;

        public NoteService(IRepositoryWrapper wrapper, IMapper mapper)
        {
            this.wrapper = wrapper;
            this.mapper = mapper;
        }
    }
}
