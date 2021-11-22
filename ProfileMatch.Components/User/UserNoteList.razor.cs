using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Components.Dialogs;
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
    public partial class UserNoteList
    {
        [Inject] public ISnackbar Snackbar { get; set; }
        [CascadingParameter] private Task<AuthenticationState> AuthenticationStateTask { get; set; }
        [Inject] private IDialogService DialogService { get; set; }
        List<Note> Notes = new();
        List<UserNote> UserNotes = new();
        [Inject] DataManager<Note, ApplicationDbContext> NoteRepository { get; set; }
        [Inject] DataManager<UserNote, ApplicationDbContext> UserNoteRepository { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        string UserId;
        private async Task<List<Note>> GetNotesAsync()
        {
            return await NoteRepository.Get();
        }
        private async Task<List<UserNote>> GetUserNotesAsync()
        {
            return await UserNoteRepository.Get(u=>u.ApplicationUserId==UserId);
        }

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateTask;
            if (authState.User.Identity.IsAuthenticated)
            {
                UserId = authState.User.Claims.FirstOrDefault().Value;
            }
            else
            {
                NavigationManager.NavigateTo("Identity/Account/Login", true);
            }
            UserNotes = await GetUserNotesAsync();
            Notes = await GetNotesAsync();

            foreach (var note in Notes)
            {
                UserNoteVM userNoteVM;
                UserNote userNote;
                try
                {
                    userNote = UserNotes.FirstOrDefault(un => un.NoteId == note.Id);
                }
                catch (Exception ex)
                {

                    userNote = new();
                }
                
                if (userNote!=null)
                {
                     userNoteVM = new UserNoteVM
                    {
                        UserId = UserId,
                        UserDescription = userNote.Description,
                        IsDisplayed = false,
                        NoteId = note.Id,
                        NoteName = note.Name,
                        NoteDescription = note.Description
                    };
                }
                else
                {
                     userNoteVM = new UserNoteVM
                    {
                        UserId = UserId,
                        UserDescription = String.Empty,
                        IsDisplayed = false,
                        NoteId = note.Id,
                        NoteName = note.Name,
                        NoteDescription = note.Description
                    };
                }
                UserNotesVM.Add(userNoteVM);
            }
        }

        private bool dense = false;
        private bool hover = true;
        private bool striped = false;
        private bool bordered = false;
        private string searchString1 = "";
        private UserNoteVM selectedItem1 = null;
        private readonly List<UserNoteVM> UserNotesVM = new();

        private bool FilterFunc1(UserNoteVM UserNoteVM) => FilterFunc(UserNoteVM, searchString1);

        private static bool FilterFunc(UserNoteVM UserNoteVM, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (UserNoteVM.NoteName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (UserNoteVM.NoteDescription != null && UserNoteVM.NoteDescription.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (UserNoteVM.UserDescription != null && UserNoteVM.UserDescription.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }

        private async Task UserNoteUpdate(UserNoteVM UserNoteVM)
        {
            var parameters = new DialogParameters { ["EditUserNote"] = UserNoteVM };
            var dialog = DialogService.Show<UserNoteDialog>("Update Note", parameters);
            await dialog.Result;
        }



        [Inject]
        private IStringLocalizer<LanguageService> L { get; set; }
    }
}
