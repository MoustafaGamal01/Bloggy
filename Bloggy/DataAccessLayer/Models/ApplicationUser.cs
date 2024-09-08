namespace Bloggy.DataAccessLayer.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public string DisplayName { get; set; }

        public byte[]? ProfilePicture { get; set; }

        public bool isBanned { get; set; } = false;
    }
}
