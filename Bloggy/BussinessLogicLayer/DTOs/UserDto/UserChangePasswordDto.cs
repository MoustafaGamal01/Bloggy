namespace Bloggy.BussinessLogicLayer.DTOs.UserDto
{
    public class UserChangePasswordDto
    {
        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}
