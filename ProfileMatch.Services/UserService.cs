using System.Collections.Generic;
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
            ServiceResponse<List<ApplicationUserVM>> result = new();
            var users = await wrapper.User.FindAllAsync();
            result.Data = mapper.Map<List<ApplicationUser>, List<ApplicationUserVM>>(users.Data);
            result.Message = users.Message;
            result.Success = users.Success;
            return result;
        }

        public async Task<ServiceResponse<ApplicationUserVM>> Create(ApplicationUserVM user)
        {
            ServiceResponse<ApplicationUserVM> result = new();
            var doesExist = await wrapper.User.FindSingleByConditionAsync(u => u.NormalizedEmail.Equals(user.Email.ToUpper()));
            if (!doesExist.Success)
            {
                var userResult = mapper.Map<ApplicationUser>(user);
                var response = await wrapper.User.Create(userResult);
                result.Data = mapper.Map<ApplicationUserVM>(response.Data);
                result.Message = response.Message;
                result.Success = response.Success;
                return result;
            }
            else
            {
                result.Message = "User already exists.";
                result.Success = false;
                return result;
            }
        }

        public async Task<ServiceResponse<ApplicationUserVM>> Delete(string id)
        {
            ServiceResponse<ApplicationUserVM> result = new();
            var doesExist = await wrapper.User.FindSingleByConditionAsync(u => u.Id == id);

            var response = wrapper.User.Delete(doesExist.Data);
            result.Data = mapper.Map<ApplicationUserVM>(response);
            result.Success = response.Success;
            result.Message = response.Message;
            return result;
        }

        public async Task<ServiceResponse<ApplicationUserVM>> Update(ApplicationUserVM userVM)
        {
                ServiceResponse<ApplicationUserVM> result = new();
            if (await Exist(userVM.Email))
            {
                var user = mapper.Map<ApplicationUserVM, ApplicationUser>(userVM);
                var response = await wrapper.User.Update(user, userVM.Id);
                result.Data = mapper.Map<ApplicationUser, ApplicationUserVM>(response.Data);
                result.Message = response.Message;
                result.Success = response.Success;
            }
            else
            {
                result.Message = "User with provided data doesn't exist.";
                result.Success = false;
            }
                return result;
        }

        public async Task<ServiceResponse<ApplicationUserVM>> FindSingleByIdAsync(string id)
        {
            ServiceResponse<ApplicationUserVM> result = new();
            var doesExist = await wrapper.User.FindSingleByConditionAsync(u => u.Id == id);
            result.Data = mapper.Map<ApplicationUser, ApplicationUserVM>(doesExist.Data);
            result.Message = doesExist.Message;
            result.Success = doesExist.Success;
            return result;
        }
        public async Task<ServiceResponse<ApplicationUserVM>> FindSingleByEmailAsync(string email)
        {
            ServiceResponse<ApplicationUserVM> result = new();
            var response = await wrapper.User.FindSingleByConditionAsync(u => u.NormalizedEmail == email.ToUpper());
            result.Data = mapper.Map<ApplicationUser, ApplicationUserVM>(response.Data);
            result.Message = response.Message;
            result.Success = response.Success;
            return result;
        }

        public async Task<bool> Exist(ApplicationUserVM editUserVM)
        {
            return await wrapper.User.Exist(u => u.Id == editUserVM.Id);
        }

        public async Task<bool> Exist(string email)
        {
            return await wrapper.User.Exist(u => u.NormalizedEmail == email.ToUpper());
        }


    }
}
