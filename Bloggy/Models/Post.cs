using System.ComponentModel.DataAnnotations.Schema;

namespace Bloggy.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MaxLength(2000)]
        public string Content { get; set; }
        public byte[]? Image { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        public int TimeToRead { get; set; }

        // Nav Properties
        [ForeignKey("User")]
        public string? UserId { get; set; }
        public ApplicationUser User { get; set; }

        //fk
        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public Post()
        {
            Comments = new List<Comment>();
        }
    }
}
