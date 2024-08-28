namespace Bloggy.DTOs.PostDto
{
    public class PostAddDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public byte[]? Img { get; set; }

        public int TimeToRead { get; set; }

        public int CategoryId{ get; set; }
    }
}
