using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

using MudBlazor;

using ProfileMatch.Contracts;
using ProfileMatch.Models.Models;

namespace ProfileMatch.Components.Dialogs
{
    public partial class EditDepartmentDialog : ComponentBase
    {
        
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [Parameter] public Department Dep { get; set; } = new();
        public string TempName { get; set; }
        public string TempDescription { get; set; }
        protected override void OnInitialized()
        {
            TempName = Dep.Name;
            TempDescription = Dep.Description;
        }
        [Inject] public IDepartmentRepository DepartmentRepository { get; set; }

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
                Dep.Name = TempName;
                Dep.Description = TempDescription;
                await DepartmentRepository.Update(Dep);
               MudDialog.Close(DialogResult.Ok(Dep));   
            }
        }

        private bool _success;

        
    }
}