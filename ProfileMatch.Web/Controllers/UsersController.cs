using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ProfileMatch.Contracts;
using ProfileMatch.Models.ViewModels;

namespace ProfileMatch.Web.Controllers
{
    //test controller for api alternative
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IMapper mapper;

        public UsersController(IRepositoryWrapper repoWrapper, IMapper mapper)
        {
            _repoWrapper = repoWrapper;
            this.mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<ApplicationUserVM> GetAll()
        {
            var peopleResponseList = _repoWrapper.User.FindAllAsync();
            return mapper.Map<List<ApplicationUserVM>>(peopleResponseList);
        }
        [Route("[action]", Name = "GetUsers")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var users = await _repoWrapper.User.FindAllAsync();
                var usersResult = mapper.Map<IEnumerable<ApplicationUserVM>>(users);
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
