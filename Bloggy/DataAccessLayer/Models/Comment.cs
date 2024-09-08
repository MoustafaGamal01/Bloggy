namespace Bloggy.DataAccessLayer.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(2000)]
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        // Nav Properties
        //[Required]
        [ForeignKey("Post")]
        public int? PostId { get; set; }
        public Post Post { get; set; }

        [ForeignKey("User")]
        public string? UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
