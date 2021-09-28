using System;
using System.Collections.Generic;
using System.Threading.Tasks;



using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ProfileMatch.Contracts;


namespace ProfileMatch.Web.Controllers
{
    //test controller for api alternative
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;
        

        public UsersController(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
            
        }

        [HttpGet]
        public IEnumerable<ApplicationUser> GetAll()
        {
            var peopleResponseList = _repoWrapper.User.FindAllAsync();
            return mapper.Map<List<ApplicationUser>>(peopleResponseList);
        }
        [Route("[action]", Name = "GetUsers")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var users = await _repoWrapper.User.FindAllAsync();
                var usersResult = mapper.Map<IEnumerable<ApplicationUser>>(users);
                return Ok(usersResult);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error {ex.Message}");
            }

        }
        [HttpGet("[action]", Name = "GetUserById")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            try
            {
                var user = await _repoWrapper.User.FindSingleByConditionAsync(x => x.Id == id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error {ex.Message}");
            }
        }
    }
}
