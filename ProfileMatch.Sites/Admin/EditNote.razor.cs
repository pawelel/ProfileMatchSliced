using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Components;

using MudBlazor;

using ProfileMatch.Models.Models;

namespace ProfileMatch.Sites.Admin
{
    public partial class EditNote : ComponentBase
    {
        bool _success;
        Note note = new();
        [Parameter] public int Id { get; set; }
        MudForm Form;
    }
}
