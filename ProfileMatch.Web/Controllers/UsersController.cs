using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using ProfileMatch.Contracts;
using ProfileMatch.Models.Models;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileMatch.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public UsersController(IRepositoryWrapper repoWrapper, ILogger logger, IMapper mapper)
        {
            _repoWrapper = repoWrapper;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<ApplicationUser> Get()
        {
            var people = _repoWrapper.User.FindAll();
            return people;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var users = await _repoWrapper.User.FindAllAsync();
                logger.LogInformation($"Returned all users from database.");
                var usersResult = mapper.Map<IEnumerable<UserVM>>(users);
                return Ok(usersResult);
            }
            catch (Exception ex)
            {

                logger.LogError($"Something went wrong inside FindAllAsync action: {ex.Message}");
            }
        }
    }
}
