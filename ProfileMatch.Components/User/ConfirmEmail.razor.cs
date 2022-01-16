using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Components;

namespace ProfileMatch.Components.User
{
    public partial class ConfirmEmail : ComponentBase
    {
        [Parameter] [SupplyParameterFromQuery(Name ="userId")] public string  UserId { get; set; }
        [Parameter] [SupplyParameterFromQuery(Name="code")] public string  Code { get; set; }


    }
}
