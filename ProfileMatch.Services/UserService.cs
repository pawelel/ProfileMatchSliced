using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<IEnumerable<EditUserVM>> FindAllAsync()
        {
            var users = await wrapper.User.FindAllAsync();

            var userResult = mapper.Map<IEnumerable<EditUserVM>>(users);
            return userResult;
        }

        public async Task Create(EditUserVM user)
        {
            var doesExist = await wrapper.User.FindSingleByConditionAsync(u => u.NormalizedEmail.Equals(user.Email.ToUpper()));
            if (doesExist == null)
            {
                var userResult = mapper.Map<ApplicationUser>(user);
                wrapper.User.Create(userResult);
            }
        }

        public async Task Delete(string id)
        {
            var doesExist = await wrapper.User.FindSingleByConditionAsync(u => u.Id == id);
            if (doesExist != null)
            {
                wrapper.User.Delete(doesExist);
            }
        }

        public async Task Update(EditUserVM user)
        {
            var doesExist = await wrapper.User.Exist(u=>u.Id==user.Id);
            if (doesExist)
            {
                var userResult = mapper.Map<ApplicationUser>(user);

                wrapper.User.Update(userResult);
            }
        }

        public async Task<EditUserVM> FindSingleByIdAsync(string id)
        {
            var test = await wrapper.User.FindSingleByConditionAsync(u => u.Id == id);
            var userResult = mapper.Map<EditUserVM>(test);
            return userResult;
        }
        public async Task<ApplicationUser> FindSingleByEmailAsync(string email)
        {
            return await wrapper.User.FindSingleByConditionAsync(u => u.NormalizedEmail==email.ToUpper());
        }

        public async Task<bool> Exist(EditUserVM editUserVM)
        {
            return await wrapper.User.Exist(u=>u.Id==editUserVM.Id);
        }

        public async Task<bool> Exist(string id)
        {
            return await wrapper.User.Exist(u => u.Id == id);
        }
    }
}
