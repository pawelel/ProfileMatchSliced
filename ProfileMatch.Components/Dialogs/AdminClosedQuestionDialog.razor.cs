using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using ProfileMatch.Repositories;
using ProfileMatch.Services;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileMatch.Components.Dialogs
{
    public partial class AdminClosedQuestionDialog : ComponentBase
    {
        [Inject] private ISnackbar Snackbar { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [Parameter] public ClosedQuestion Q { get; set; } = new();
        [Parameter] public int CategoryId { get; set; }

        public int ClosedQuestionId { get; set; }
        public string TempName { get; set; }
        public string TempDescription { get; set; }
        public bool TempIsActive { get; set; }
        public bool CanActivateState { get; set; } = false;

        protected override void OnInitialized()
        {
            TempName = Q.Name;
            TempDescription = Q.Description;
        }

        [Inject] DataManager<ClosedQuestion, ApplicationDbContext> ClosedQuestionRepository { get; set; }
        private MudForm Form;

        private void Cancel()
        {
            MudDialog.Cancel();
            Snackbar.Add(L["Operation cancelled"], Severity.Warning);
        }

        protected async Task HandleSave()
        {
            await Form.Validate();
            if (Form.IsValid)
            {
                Q.Name = TempName;
                Q.Description = TempDescription;
                Q.CategoryId = CategoryId;
                Q.IsActive = TempIsActive;
                try
                {
                    await Save();
                }
                catch (Exception ex)
                {
                    Snackbar.Add(@L[$"There was an error:"] + $" {ex.Message}", Severity.Error);
                }

                MudDialog.Close(DialogResult.Ok(Q));
            }
        }
        private async Task Save()
        {//has any other question the same name in the category?
            var exists = (await ClosedQuestionRepository.Get(q => q.Name == Q.Name));
            if (Q.Id == 0 && exists.Count == 0)
            {
                var result = await ClosedQuestionRepository.Insert(Q);
                if (ShareResource.IsEn())
                {
                    Snackbar.Add($"Question {result.Name} has been created", Severity.Success);
                }
                else
                {
                    Snackbar.Add($"Pytanie {result.Name} zostało utworzone", Severity.Success);
                }
            }
            else if (exists.Count <= 1)
            {
                await ClosedQuestionRepository.Update(Q);
                if (ShareResource.IsEn())
                {
                    Snackbar.Add($"Question {Q.Name} has been updated", Severity.Success);
                }
                else
                {
                    Snackbar.Add($"Pytanie  {Q.Name} zostało zaktualizowane", Severity.Success);
                }
            }
            else
            {
                if (ShareResource.IsEn())
                {
                    Snackbar.Add($"Question{Q.Name} already exists", Severity.Error);
                }
                else
                {
                    Snackbar.Add($"Pytanie  {Q.Name} już istnieje", Severity.Error);
                }
            }
        }
        [Inject] private IStringLocalizer<LanguageService> L { get; set; }
    }
}