using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using ProfileMatch.Models;
using ProfileMatch.Models.Models;
using ProfileMatch.Models.Profiles;
using ProfileMatch.Models.ViewModels;
using ProfileMatch.Components;
using ProfileMatch.Services;
using ProfileMatch.Contracts;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using ProfileMatch;
using ProfileMatch.Components.Culture;
using ProfileMatch.Components.Layout;
using ProfileMatch.Components.Theme;
using Microsoft.Extensions.Localization;
using ProfileMatch.Models.Enumerations;
using Microsoft.AspNetCore.Components;
using ProfileMatch.Repositories;
using System.Threading.Tasks;

namespace ProfileMatch.Components.Admin
{
    public partial class DepartmentsList : ComponentBase
    {
        [Inject]
        public IDepartmentService DepartmentService { get; set; }
        [Parameter] public int id { get; set; }

        private IEnumerable<DepartmentVM> Departments;
        protected override async Task OnInitializedAsync()
        {
            Departments = await DepartmentService.GetDepartmentsWithPeople();
        }

        
       
    }
}
