using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

using ProfileMatch.Contracts;
using ProfileMatch.Models.Models;

namespace ProfileMatch.Components.Manager
{
    public partial class CompareList : ComponentBase
    {
        [Inject]
        public IUserRepository UserRepo { get; set; }

        [Inject]
        private NavigationManager NavMan { get; set; }

        public string SearchString { get; set; }

        private bool _dense = false;
        public bool disabled = false;

        private bool FilterFunc1(ApplicationUser person) => FilterFunc(person, SearchString);

        private static bool FilterFunc(ApplicationUser person, string searchString)
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

        public List<ApplicationUser> Users { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Users = await UserRepo.GetAll();
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