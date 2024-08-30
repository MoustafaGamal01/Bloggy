namespace Bloggy.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public string DisplayName { get; set; }
        public byte[]? ProfilePicture { get; set; }
    }
}
