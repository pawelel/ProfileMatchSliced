using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using ProfileMatch.Contracts;
using ProfileMatch.Models.Models;
using ProfileMatch.Models.Responses;
using ProfileMatch.Models.ViewModels;

namespace ProfileMatch.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryWrapper wrapper;
        private readonly IMapper mapper;
        public UserService(IRepositoryWrapper wrapper, IMapper mapper)
        {
            this.wrapper = wrapper;
            this.mapper = mapper;
        }

        public async Task<ServiceResponse<List<ApplicationUserVM>>> FindAllAsync()
        {
               var users = await wrapper.User.FindAllAsync();
            return mapper.Map<ServiceResponse<List<ApplicationUserVM>>>(users);
        }

        public async Task<ServiceResponse<ApplicationUserVM>> Create(ApplicationUserVM user)
        {
            var doesExist = await wrapper.User.FindSingleByConditionAsync(u => u.NormalizedEmail.Equals(user.Email.ToUpper()));
            if (!doesExist.Success)
            {
                var userResult = mapper.Map<ApplicationUser>(user);
               var response = wrapper.User.Create(userResult);
                return mapper.Map<ServiceResponse<ApplicationUserVM>>(response);
            }
            else
            {
                return mapper.Map<ServiceResponse<ApplicationUserVM>>(doesExist);
            }
        }

        public async Task<ServiceResponse<ApplicationUserVM>> Delete(string id)
        {
            var doesExist = await wrapper.User.FindSingleByConditionAsync(u => u.Id == id);
           
              var response =  wrapper.User.Delete(doesExist.Data);
            return mapper.Map<ServiceResponse<ApplicationUserVM>>(response);
        }

        public ServiceResponse<ApplicationUserVM> Update(ApplicationUserVM userVM)
        {
            var user = mapper.Map<ServiceResponse<ApplicationUser>>(userVM);
            var response = wrapper.User.Update(user.Data);
            return mapper.Map<ServiceResponse<ApplicationUserVM>>(response);
        }

        public async Task<ServiceResponse<ApplicationUserVM>> FindSingleByIdAsync(string id)
        {
            var test = await wrapper.User.FindSingleByConditionAsync(u => u.Id == id);
           return mapper.Map<ServiceResponse<ApplicationUserVM>>(test);
            
        }
        public async Task<ServiceResponse<ApplicationUserVM>> FindSingleByEmailAsync(string email)
        {
            var response= await wrapper.User.FindSingleByConditionAsync(u => u.NormalizedEmail==email.ToUpper());
            return mapper.Map<ServiceResponse<ApplicationUserVM>>(response);
        }

        public async Task<bool> Exist(ApplicationUserVM editUserVM)
        {
            return await wrapper.User.Exist(u=>u.Id==editUserVM.Id);
        }

        public async Task<bool> Exist(string id)
        {
            return await wrapper.User.Exist(u => u.Id == id);
        }

        
    }
}
