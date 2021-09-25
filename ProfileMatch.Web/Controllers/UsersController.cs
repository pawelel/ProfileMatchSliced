using Microsoft.AspNetCore.Mvc;

using ProfileMatch.Contracts;
using ProfileMatch.Models.Models;

using System.Collections.Generic;

namespace ProfileMatch.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;

        public UsersController(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }

        [HttpGet]
        public IEnumerable<ApplicationUser> Get()
        {
            var people = _repoWrapper.User.FindAll();
            return people;
        }
    }
}
