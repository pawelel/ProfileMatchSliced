﻿using Microsoft.AspNetCore.Components;

using MudBlazor;

namespace ProfileMatch.Components.Authentication
{
    public partial class Register : ComponentBase
    {
        //private readonly UserManager<ApplicationUser> _userManager;

        private string Password { get; set; }

        private bool PasswordVisibility;
        private InputType PasswordInput = InputType.Password;
        private string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;

        private void TogglePasswordVisibility()
        {
            if (PasswordVisibility)
            {
                PasswordVisibility = false;
                PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
                PasswordInput = InputType.Password;
            }
            else
            {
                PasswordVisibility = true;
                PasswordInputIcon = Icons.Material.Filled.Visibility;
                PasswordInput = InputType.Text;
            }
        }
    }
}