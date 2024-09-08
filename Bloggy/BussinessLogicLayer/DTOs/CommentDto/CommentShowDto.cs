namespace Bloggy.BussinessLogicLayer.DTOs.CommentDto
{
    public class CommentShowDto
    {
        public string Username { get; set; }
        public string Content { get; set; }
        public byte[]? Img { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
