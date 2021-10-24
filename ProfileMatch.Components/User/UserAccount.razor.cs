using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileMatch.Components.User
{
   public partial class UserAccount :ComponentBase
    {
        [Inject]
        ISnackbar Snackbar { get; set; }

        public string AvatarImageLink { get; set; } = "https://randomuser.me/api/portraits/men/1.jpg";
        public string AvatarIcon { get; set; }
        public string AvatarButtonText { get; set; } = "Delete Picture";
        public Color AvatarButtonColor { get; set; } = Color.Error;
        public string FirstName { get; set; } = "Karol";
        public string LastName { get; set; } = "Pluciński";
        public string JobTitle { get; set; } = "IT Consultant";
        public string Email { get; set; } = "karol@test.com";
        public bool FriendSwitch { get; set; } = true;



        void DeletePicture()
        {
            if (!String.IsNullOrEmpty(AvatarImageLink))
            {
                AvatarImageLink = null;
                AvatarIcon = Icons.Material.Outlined.SentimentVeryDissatisfied;
                AvatarButtonText = "Upload Picture";
                AvatarButtonColor = Color.Primary;
            }
            else
            {
                return;
            }
        }

        void SaveChanges(string message, Severity severity)
        {
            Snackbar.Add(message, severity, config =>
            {
                config.ShowCloseIcon = false;
            });
        }

        MudForm form;
        MudTextField<string>
        pwField1;


        [Inject]
        IStringLocalizer<LanguageService> L { get; set; }
    }
}
