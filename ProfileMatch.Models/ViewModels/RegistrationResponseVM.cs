using System.Collections.Generic;

namespace ProfileMatch.Models.ViewModels
{
    public class RegistrationResponseVM
    {
        public bool IsSuccessfulRegistration { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}