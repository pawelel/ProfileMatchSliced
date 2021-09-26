using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

using ProfileMatch.Contracts;
using ProfileMatch.Models.Models;
using ProfileMatch.Models.ViewModels;

namespace ProfileMatch.Components.Admin
{
    public partial class PeopleList : ComponentBase
    {
        [Inject]
        public IUserService UserService { get; set; }

        [Inject]
        private NavigationManager NavMan { get; set; }

        public string SearchString { get; set; }

        private bool _dense = false;
        public bool disabled = false;

        private bool FilterFunc1(EditUserVM person) => FilterFunc(person, SearchString);

        private static bool FilterFunc(EditUserVM person, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (person.Department.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (person.FirstName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (person.LastName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if ($"{person.DateOfBirth} {person.IsActive}".Contains(searchString))
                return true;
            return false;
        }

        public IEnumerable<EditUserVM> Users { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Users = await UserService.FindAllAsync();
        }

        private void ShowProfile(string id)
        {
            NavMan.NavigateTo($"/admin/users/{id}");
        }

        private void EditProfile(string id)
        {
            NavMan.NavigateTo($"/admin/edituser/{id}");
        }
    }
}