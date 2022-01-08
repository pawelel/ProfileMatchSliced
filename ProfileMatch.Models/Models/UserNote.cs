using Ganss.Excel;

namespace ProfileMatch.Models.Models
{
    public class UserOpenAnswer
    {
        public string ApplicationUserId { get; set; }
        [Ignore]
        public ApplicationUser ApplicationUser { get; set; }
        [Ignore]
        public OpenQuestion OpenQuestion { get; set; }
        public int OpenQuestionId { get; set; }
        public bool IsDisplayed { get; set; }
        public string UserAnswer { get; set; }
    }
}