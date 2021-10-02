using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

using MudBlazor;

using ProfileMatch.Contracts;

using ProfileMatch.Models.Models;

namespace ProfileMatch.Components.Dialogs
{
    public partial class EditCategoryDialog : ComponentBase
    {
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [Parameter] public Category Cat { get; set; } = new();
        public string TempName { get; set; }
        public string TempDescription { get; set; }
        protected override void OnInitialized()
        {
            TempName = Cat.Name;
            TempDescription = Cat.Description;
        }
        [Inject] public ICategoryRepository CategoryRepository { get; set; }

        private MudForm Form;
        private void Cancel()
        {
            MudDialog.Cancel();
        }
        protected async Task HandleSave()
        {
            await Form.Validate();
            if (Form.IsValid)
            {
                Cat.Name = TempName;
                Cat.Description = TempDescription;
                await CategoryRepository.Update(Cat);
                MudDialog.Close(DialogResult.Ok(Cat));
            }
        }
    }
}

