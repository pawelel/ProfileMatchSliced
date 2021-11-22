using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using MudBlazor;
using ProfileMatch.Components.Dialogs;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;

using ProfileMatch.Repositories;

using ProfileMatch.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ProfileMatch.Components.Admin
{
    public partial class AdminNoteList
    {
        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        private IDialogService DialogService { get; set; }

        [Inject] DataManager<Note, ApplicationDbContext> NoteRepository { get; set; }

        private async Task<List<Note>> GetNotesAsync()
        {
            return await NoteRepository.Get();
        }

        protected override async Task OnInitializedAsync()
        {
            Notes = await GetNotesAsync();
        }

        private bool dense = false;
        private bool hover = true;
        private bool striped = false;
        private bool bordered = false;
        private string searchString1 = "";
        private Note selectedItem1 = null;
        private List<Note> Notes = new();

        private bool FilterFunc1(Note Note) => FilterFunc(Note, searchString1);

        private static bool FilterFunc(Note Note, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (Note.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (Note.Description != null && Note.Description.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }

        private async Task NoteUpdate(Note Note)
        {
            var parameters = new DialogParameters { ["Cat"] = Note };
            var dialog = DialogService.Show<AdminNoteDialog>("Update Note", parameters);
            await dialog.Result;
        }

        private async Task NoteCreate()
        {
            var dialog = DialogService.Show<AdminNoteDialog>("Create Note");
            await dialog.Result;
            Notes = await GetNotesAsync();
        }

        

        [Inject]
        private IStringLocalizer<LanguageService> L { get; set; }
    }
}
