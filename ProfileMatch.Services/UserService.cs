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
    public class UserService : IUserService
    {
        private IRepositoryWrapper wrapper;
        private readonly IMapper mapper;
        public UserService(IRepositoryWrapper wrapper, IMapper mapper)
        {
            this.wrapper = wrapper;
            this.mapper = mapper;
        }

        public async Task<List<ApplicationUser>> GetAllUsers()
        {
            var users = await wrapper.User.FindAllAsync();
            if (users == null)
            {
                return new();
            }
            return users.ToList();
        }

        public async Task CreateUser(EditUserVM user)
        {
            var doesExist = await wrapper.User.FindSingleByConditionAsync(u => u.NormalizedEmail.Equals(user.Email.ToUpper()));
            if (doesExist == null)
            {
                var userResult = mapper.Map<ApplicationUser>(user);
                wrapper.User.Create(userResult);
            }
        }

        public async Task DeleteUser(string id)
        {
            var doesExist = await wrapper.User.FindSingleByConditionAsync(u => u.Id == id);
            if (doesExist != null)
            {
                wrapper.User.Delete(doesExist);
            }
        }

        public async Task UpdateUser(EditUserVM user)
        {
            var doesExist = await wrapper.User.FindSingleByConditionAsync(u => u.NormalizedEmail.Equals(user.Email.ToUpper()));
            if (doesExist != null)
            {
                var userResult = mapper.Map<ApplicationUser>(user);

                wrapper.User.Update(userResult);
            }
        }
    }
}
