namespace Bloggy.DTOs.PostDto
{
    public class PostShowDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public byte[]? Img { get; set; }

        public DateTime CreatedAt { get; set; }

        public int TimeToRead { get; set; } 

        public string? UserName { get; set; }

        public string? Category { get; set; }

        public ICollection<CommentShowDto>? Comments { get; set; }
    }
}
