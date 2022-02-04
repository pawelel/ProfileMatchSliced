using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using MudBlazor;
using ProfileMatch.Models.Entities;

using ProfileMatch.Repositories;

namespace ProfileMatch.Components.Admin.Dialogs
{
    public partial class AdminJobListDialog : ComponentBase
    {
        bool _loading;
        [Inject] IUnitOfWork UnitOfWork { get; set; }
        [Inject] private IDialogService DialogService { get; set; }
        private IEnumerable<Job> _jobs = new List<Job>();
        private string _searchString;
        protected override async Task OnInitializedAsync()
        {
            _loading = true;
            _jobs = await UnitOfWork.Jobs.Get();
            _loading = false;
        }
        // quick filter - filter gobally across multiple columns with the same input
        private Func<Job, bool> QuickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;

            if (x.Name.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x.NamePl.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        };
        private async Task JobUpdate(Job Job = null)
        {
            if (Job == null)
            {
                var dialog = DialogService.Show<AdminJobDialog>(L["Create Job"]);
                await dialog.Result;
            }
            else
            {
                var parameters = new DialogParameters { ["JobId"] = Job.Id };
                var dialog = DialogService.Show<AdminJobDialog>(L["Edit Job"], parameters);
                await dialog.Result;
            }

        }
    }
}
