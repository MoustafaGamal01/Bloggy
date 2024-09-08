namespace Bloggy.BussinessLogicLayer.DTOs.PostDto
{
    public class PostUpdateDto
    {
        public string? Title { get; set; }

        public string? Content { get; set; }

        public int TimeToRead { get; set; }

        public int? CategoryId { get; set; }
    }
}
