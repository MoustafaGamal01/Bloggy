namespace Bloggy.BussinessLogicLayer.DTOs.CategoryDto
{
    public class CategoryAddDto
    {
        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}
