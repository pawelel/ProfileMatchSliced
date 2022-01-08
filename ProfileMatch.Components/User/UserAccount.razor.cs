using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using ProfileMatch.Models.ViewModels;
using ProfileMatch.Repositories;
using ProfileMatch.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileMatch.Components.User
{
    public partial class UserAccount : ComponentBase
    {
        public string AvatarImageLink { get; set; }
        public string AvatarIcon { get; set; }
        private List<UserAnswerVM> UserOpenAnswersVM;
        [Inject] DataManager<UserOpenAnswer, ApplicationDbContext> UserOpenAnswerRepository { get; set; }
        [Parameter] public string UserId { get; set; }
        [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        ApplicationUser CurrentUser;
        [Inject] DataManager<ApplicationUser, ApplicationDbContext> AppUserManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(UserId))
            {
                CurrentUser = await AppUserManager.GetById(UserId);
            }
            else
            {
                var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                var principal = authState.User;
                if (principal != null)
                    UserId = principal.FindFirst("UserId").Value;
                CurrentUser = await AppUserManager.GetById(UserId);
            }
            UserOpenAnswersVM = await GetUserAnswerVMAsync();
        }
        private async Task<List<UserAnswerVM>> GetUserAnswerVMAsync()
        {
            List<UserOpenAnswer> answers = new();
                answers = await UserOpenAnswerRepository.Get(u => u.ApplicationUserId == UserId, include: src => src.Include(n => n.OpenQuestion));
            if (answers != null)
            {
                answers = (from n in answers where n.IsDisplayed == true select n).ToList();
                List<UserAnswerVM> userAnswersVM = new();
                foreach (var answer in answers)
                {
                    var answerVM = new UserAnswerVM()
                    {
                        IsDisplayed = answer.IsDisplayed,
                        OpenQuestionDescription = answer.OpenQuestion.Description,
                        AnswerId = answer.OpenQuestionId,
                        OpenQuestionName = answer.OpenQuestion.Name,
                        UserDescription = answer.UserAnswer,
                        UserId = answer.ApplicationUserId
                    };
                    userAnswersVM.Add(answerVM);
                }
                return userAnswersVM;
            }return new();
        }
    }
}