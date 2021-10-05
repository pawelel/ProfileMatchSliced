namespace ProfileMatch.Models.Models
{
    public class UserNeedCategory
    {
        public bool Want { get; set; }
        public string ApplicationUserId { get; set; }
        public int CategoryId { get; set; }
        public  Category Category { get; set; }
        public  ApplicationUser ApplicationUser { get; set; }
    }
}