namespace Bloggy.DTOs.AuthDto
{
    public class RegisterDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public IFormFile? ProfilePicture { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
