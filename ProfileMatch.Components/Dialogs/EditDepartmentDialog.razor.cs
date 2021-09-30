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
        [Inject] private ISnackbar Snackbar { get; set; }

        [Inject] public IDepartmentService DepartmentService { get; set; }

        private MudForm Form;

        protected async Task HandleSave()
        {
            await Form.Validate();
            if (Form.IsValid)
            {
                if (await DepartmentService.Exist(Dep))
                {
                    await DepartmentService.Update(Dep);
                }
                else
                {
                    await DepartmentService.Create(Dep);
                }
                    MudDialog.Close();
            }
        }

        private bool _success;

        private void Cancel()
        {
            MudDialog.Cancel();
        }
    }
}