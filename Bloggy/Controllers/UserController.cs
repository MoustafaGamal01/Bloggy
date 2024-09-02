using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bloggy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Admin")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(IUserService userService, RoleManager<IdentityRole> roleManager)
        {
            _userService = userService;
            _roleManager = roleManager;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddUser([FromForm]RegisterDto userAddDto)
        {
            var existingUser = await _userService.GetUserByEmail(userAddDto.Email);

            if (existingUser != null)
            {
                return BadRequest($"User with email {userAddDto.Email} already exists");
            }

            byte[]? Image = null;
            if (userAddDto.ProfilePicture != null)
            {
                if (userAddDto.ProfilePicture.Length > 2 * 1024 * 1024)
                {
                    return BadRequest("Profile picture must be 2mb max.");
                }
                using (var memoryStream = new MemoryStream())
                {
                    await userAddDto.ProfilePicture.CopyToAsync(memoryStream);
                    Image = memoryStream.ToArray();
                }
            }

            var user = new ApplicationUser
            {
                Email = userAddDto.Email,
                UserName = userAddDto.Email,
                DisplayName = userAddDto.Name,
                ProfilePicture = Image
            };

            var result = await _userService.AddUser(user, userAddDto.Password);

            if (result.Succeeded)
            {
                await _userService.AddUserToRole(user, "Admin");
                return Ok("Admin added successfully");
            }

            return BadRequest("Failed to add user");
        }

        [HttpGet]
        [Route("get/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _userService.GetUserByEmail(email);

            if (user == null)
            {
                return BadRequest($"Can't find user with email {email}");
            }
            
            var roles = await _userService.GetUserRoles(user); 

            var userDto = new UserShowDto
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.DisplayName,
                Role = roles.FirstOrDefault()
            };
            
            return Ok(userDto);
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            var userDtos = new List<UserShowDto>();

            foreach (var user in users)
            {
                var roles = await _userService.GetUserRoles(user);
                var userDto = new UserShowDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.DisplayName,
                    Role = roles.FirstOrDefault()
                };
                userDtos.Add(userDto);
            }

            return Ok(userDtos);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateUser([FromForm]UserUpdateDto userUpdateDto)
        {
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var user = await _userService.GetUserByEmail(userEmail);

            if (user == null)
            {
                return BadRequest($"Can't find user with email {userEmail}");
            }

            if (userUpdateDto.Email == null && userUpdateDto.Name == null && user.ProfilePicture == null)
            {
                return BadRequest("Please provide email or name to update");
            }

            if(userUpdateDto.ProfilePic != null)
            {
                if (userUpdateDto.ProfilePic.Length > 2 * 1024 * 1024)
                {
                    return BadRequest("Profile picture must be 2mb max.");
                }
                byte[]? Image = null;
                using (var memoryStream = new MemoryStream())
                {
                    await userUpdateDto.ProfilePic.CopyToAsync(memoryStream);
                    Image = memoryStream.ToArray();
                }
                user.ProfilePicture = Image;
            }

            if(userUpdateDto.Email != null)
            {
                var existingUser = await _userService.GetUserByEmail(userUpdateDto.Email);
                if (existingUser != null)
                {
                    return BadRequest($"User with email {userUpdateDto.Email} already exists");
                }
            }
            if(userUpdateDto.Name != null) user.DisplayName = userUpdateDto.Name;

            var result = await _userService.UpdateUser(user);

            if (result.Succeeded)
            {
                return Ok("User updated successfully");
            }

            return BadRequest("Failed to update user");
        }

        [HttpPut]
        [Route("changePassword")]
        public async Task<IActionResult> ChangePassword(UserChangePasswordDto changePasswordDto)
        {
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            var user = await _userService.GetUserByEmail(userEmail);

            if (user == null)
            {
                return BadRequest($"Can't find user with email {userEmail}");
            }

            var result = await _userService.ChangeUserPassword(user, changePasswordDto);

            if (result.Succeeded)
            {
                return Ok("Password changed successfully");
            }

            return BadRequest("Failed to change password");
        }

        [HttpPut]
        [Route("ban/{email}")]
        public async Task<IActionResult> BanUser(string email)
        {
            var user = await _userService.GetUserByEmail(email);

            if (user == null)
            {
                return BadRequest($"Can't find user with email {email}");
            }
            if(user.isBanned)
            {
                return BadRequest("User is already banned");
            }
            var roles = await _userService.GetUserRoles(user);
            if (roles.Contains("Admin"))
            {
                return BadRequest("Can't ban admin user");
            }

            user.isBanned = true;

            var result = await _userService.UpdateUser(user);

            if (result.Succeeded)
            {
                return Ok("User banned successfully");
            }

            return BadRequest("Failed to ban user");
        }

        [HttpPut]
        [Route("unban/{email}")]
        public async Task<IActionResult> UnbanUser(string email)
        {
            var user = await _userService.GetUserByEmail(email);

            if (user == null)
            {
                return BadRequest($"Can't find user with email {email}");
            }
            if(user.isBanned == false)
            {
                return BadRequest("User is not banned");
            }
            user.isBanned = false;

            var result = await _userService.UpdateUser(user);

            if (result.Succeeded)
            {
                return Ok("User unbanned successfully");
            }

            return BadRequest("Failed to unban user");
        }
    }
}
