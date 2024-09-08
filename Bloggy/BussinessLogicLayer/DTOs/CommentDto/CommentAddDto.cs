namespace Bloggy.BussinessLogicLayer.DTOs.CommentDto
{
    public class CommentAddDto
    {
        [Required]
        public string Content { get; set; }
        public int PostId { get; set; }
    }
}
