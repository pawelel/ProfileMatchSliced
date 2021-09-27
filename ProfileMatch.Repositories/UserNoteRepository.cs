﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;

namespace ProfileMatch.Repositories
{

    public class UserNoteRepository : RepositoryBase<UserNote>, IUserNoteRepository
    {
        public UserNoteRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {

        }
    }
}
