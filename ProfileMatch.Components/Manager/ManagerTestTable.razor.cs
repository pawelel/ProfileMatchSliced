using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

using MudBlazor;

using ProfileMatch.Contracts;
using ProfileMatch.Models.ViewModels;

namespace ProfileMatch.Components.Manager
{
    public partial class ManagerTestTable : ComponentBase
    {
        [Inject]
        private IUserRepository UserRepository { get; set; }

        private readonly TableGroupDefinition<QuestionUserLevelVM> _groupDefinition = new()
        {
            GroupName = "Category",
            Indentation = false,
            Expandable = true,
            IsInitiallyExpanded = false,
            Selector = (e) => e.CategoryName
        };

        private List<QuestionUserLevelVM> Elements = new();

        protected override async Task OnInitializedAsync()
        {
            Elements = await UserRepository.GetUsersWithQuestionAnswerLevel();
        }
    }
}