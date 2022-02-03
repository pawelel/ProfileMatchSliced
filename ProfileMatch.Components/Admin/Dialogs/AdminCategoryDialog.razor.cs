using AutoMapper;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

using MudBlazor;

using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.SS.Formula.Functions;

using ProfileMatch.Data;
using ProfileMatch.Models.Entities;
using ProfileMatch.Models.ViewModels;
using ProfileMatch.Repositories;
using ProfileMatch.Services;

using Serilog;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileMatch.Components.Admin.Dialogs
{
    public partial class AdminCategoryDialog : ComponentBase
    {
        [Inject] private ISnackbar Snackbar { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [Inject] IUnitOfWork UnitOfWork { get; set; }
        [Inject] ILogger Logger { get; set; }
        [Inject] IMapper Mapper { get; set; }
        [Parameter] public int CategoryId { get; set; }
        CategoryVM _categoryVM;
        public string TempName { get; set; }
        public string TempNamePl { get; set; }
        public string TempDescription { get; set; }
        public string TempDescriptionPl { get; set; }
        bool _isOpen = false;
        Category _tempCategory;
        bool _deleteEnabled;
        public void ToggleOpen()
        {
            _isOpen = !_isOpen;
        }
        protected override async Task OnInitializedAsync()
        {
            //_deleteEnabled 
            _tempCategory = await UnitOfWork.Categories.GetOne(q => q.Id == CategoryId);
            if (_tempCategory!=null)
            {
                _categoryVM = Mapper.Map<CategoryVM>(_tempCategory);
                _deleteEnabled = true;
            }
            else
            {
                _categoryVM = new();
            }
        }
        private IEnumerable<string> MaxCharacters(string ch)
        {
            if (!string.IsNullOrEmpty(ch) && 21 < ch?.Length)
                yield return L["Max 20 characters"];
        }

        private MudForm _form;

        private void Cancel()
        {
            MudDialog.Cancel();
            Snackbar.Add(L["Operation cancelled"], Severity.Warning);
        }

        protected async Task HandleSave()
        {
            await _form.Validate();
            if (_form.IsValid)
            {
               _tempCategory = Mapper.Map<Category>(_categoryVM);
                try
                {
                    await Save();
                }
                catch (Exception ex)
                {
                    Snackbar.Add(@L[$"There was an error:"] + $" {@L[ex.Message]}", Severity.Error);
                    Logger.Error("ex", ex);
                }
                MudDialog.Close(DialogResult.Ok(true));
                NavigationManager.NavigateTo("admin/dashboard/1", true);
            }
        }
        private async Task Delete()
        {
            if (_deleteEnabled)
            {
               
                await UnitOfWork.Categories.Delete(_tempCategory);
            }
            if (ShareResource.IsEn())
            {
                Snackbar.Add($"Category {_tempCategory.Name} deleted");
            }
            else
            {
                Snackbar.Add($"Kategoria {_tempCategory.NamePl} usunięta");
            }
            MudDialog.Close(DialogResult.Ok(true));
            NavigationManager.NavigateTo("admin/dashboard/1", true);
        }
        private async Task Save()
        {
            string created;
            string updated;
            if (ShareResource.IsEn())
            {
                created = $"Category {_tempCategory.Name} created";
                updated = $"Category {_tempCategory.Name} updated";
            }
            else
            {
                created = $"Kategoria {_tempCategory.NamePl} utworzona";
                updated = $"Kategoria {_tempCategory.NamePl} zaktualizowana";
            }
            if (_tempCategory.Id == 0)
            {
                var result = await UnitOfWork.Categories.Insert(_tempCategory);
                Snackbar.Add(created, Severity.Success);
            }
            else
            {
                var result = await UnitOfWork.Categories.Update(_tempCategory);
                Snackbar.Add(updated, Severity.Success);
            }
        }
    }
}