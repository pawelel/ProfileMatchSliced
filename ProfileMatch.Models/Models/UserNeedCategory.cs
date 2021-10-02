namespace ProfileMatch.Models.Models
{
    public class UserNeedCategory
    {
        public bool Want { get; set; }
        public string ApplicationUserId { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}