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
        [Inject]        private ISnackbar Snackbar { get; set; }
        public string AvatarImageLink { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        public string AvatarIcon { get; set; }
        private List<UserNoteVM> UserNotesVM;
        [Inject] DataManager<UserNote, ApplicationDbContext> UserNoteRepository { get; set; }
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
            UserNotesVM = await GetUserNotesVMAsync();
        }
        private async Task<List<UserNoteVM>> GetUserNotesVMAsync()
        {
            var notes = await UserNoteRepository.Get(u => u.ApplicationUserId == UserId, include: src => src.Include(n => n.Note));
            notes = (from n in notes where n.IsDisplayed==true select n).ToList();
            List<UserNoteVM> userNoteVMs = new();
            foreach (var note in notes)
            {
                    var noteVM = new UserNoteVM()
                    {
                        IsDisplayed = note.IsDisplayed,
                        NoteDescription = note.Note.Description,
                        NoteId = note.NoteId,
                        NoteName = note.Note.Name,
                        UserDescription = note.Description,
                        UserId = note.ApplicationUserId
                    };
                    userNoteVMs.Add(noteVM);
            }
            return userNoteVMs;
        }
        [Inject] private IStringLocalizer<LanguageService> L { get; set; }
    }
}