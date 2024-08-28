namespace Bloggy.DTOs
{
    public class CategoryModel
    {
        [Required]
        public string Name { get; set; }

        public string? Description { get; set; } 
    }
}
