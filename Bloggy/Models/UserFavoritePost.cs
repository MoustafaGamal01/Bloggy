namespace Bloggy.Models
{
    public class UserFavoritePost
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser? User { get; set; }

        [ForeignKey("Post")]
        public int PostId { get; set; }
        public Post? Post { get; set; }
    }
}
