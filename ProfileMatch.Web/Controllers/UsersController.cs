using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using ProfileMatch.Contracts;
using ProfileMatch.Models.Models;
using ProfileMatch.Models.ViewModels;

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
        public IEnumerable<ApplicationUser> GetAll()
        {
            var people = _repoWrapper.User.FindAll();
            return people;
        }
        [Route("[action]", Name = "GetUsers")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var users = await _repoWrapper.User.FindAllAsync();
                logger.LogInformation($"Returned all users from database.");
                var usersResult = mapper.Map<IEnumerable<EditUserVM>>(users);
                return Ok(usersResult);
            }
            catch (Exception ex)
            {

                logger.LogError($"Something went wrong inside FindAllAsync action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }
        [HttpGet("[action]", Name = "GetUserById")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            try
            {
                var user = await _repoWrapper.User.FindSingleByConditionAsync(x => x.Id == id);
                logger.LogInformation($"Returned user from database.");
                return Ok(user);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong inside FindAllAsync action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
