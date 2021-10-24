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
   public partial class UserAbout : ComponentBase
    {
        [Inject]
        ISnackbar Snackbar { get; set; }
        public string FirstName { get; set; } = "Łukasz";
        public string LastName { get; set; } = "Pietrzak";
        public string JobTitle { get; set; } = "IT";
        public string Email { get; set; } = "test@test.com";
        public bool FriendSwitch { get; set; } = true;
        public bool NotificationEmail_1 { get; set; } = true;
        public bool NotificationEmail_2 { get; set; }
        public bool NotificationEmail_3 { get; set; }
        public bool NotificationEmail_4 { get; set; } = true;
        public bool NotificationChat_1 { get; set; }
        public bool NotificationChat_2 { get; set; } = true;
        public bool NotificationChat_3 { get; set; } = true;
        public bool NotificationChat_4 { get; set; }




        void SaveChanges(string message, Severity severity)
        {
            Snackbar.Add(message, severity, config =>
            {
                config.ShowCloseIcon = false;
            });
        }
        [Inject]
        IStringLocalizer<LanguageService> L { get; set; }
    }
}
