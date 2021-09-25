using Microsoft.AspNetCore.Components;

namespace ProfileMatch.Components
{
    public partial class UserCard : ComponentBase
    {
        [Parameter] public string Class { get; set; }
        [Parameter] public string Style { get; set; }
    }
}