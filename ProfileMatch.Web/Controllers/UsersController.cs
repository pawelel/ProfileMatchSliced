using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ProfileMatch.Contracts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileMatch.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IRepositoryWrapper _repoWrapper;

        public UsersController(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
    }
}
