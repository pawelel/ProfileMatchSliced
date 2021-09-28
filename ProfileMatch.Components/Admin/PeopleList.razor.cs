using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

using ProfileMatch.Contracts;
using ProfileMatch.Models.Responses;


namespace ProfileMatch.Components.Admin
{
    public partial class PeopleList : ComponentBase
    {
        [Inject]
        public IUserService UserService { get; set; }

        [Inject]
        private NavigationManager Naan { get; set; }

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
        ServiceResponse<List<ApplicationUser>> usersResponse = new();
        public List<ApplicationUser> Users { get; set; }

        protected override async Task OnInitializedAsync()
        {
            usersResponse = await UserService.FindAllAsync();
            Users = usersResponse.Data;
        }

        private void ShowProfile(string id)
        {
            Naan.NavigateTo($"/admin/users/{id}");
        }

        private void EditProfile(string id)
        {
            Naan.NavigateTo($"/admin/edituser/{id}");
        }
    }
}