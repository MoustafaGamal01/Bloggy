namespace Bloggy.DTOs.UserDto
{
    public class UserUpdateDto
    {
        public string? Email { get; set; }
        public string? Name { get; set; }
        public IFormFile? ProfilePic { get; set; }
    }
}
