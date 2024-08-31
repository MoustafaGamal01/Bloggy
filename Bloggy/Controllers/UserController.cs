﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bloggy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [Route("update/{email}")]
        public async Task<IActionResult> UpdateUser(string email, UserUpdateDto userUpdateDto)
        {
            var user = await _userService.GetUserByEmail(email);

            if (user == null)
            {
                return BadRequest($"Can't find user with email {email}");
            }

            if(userUpdateDto.Email == null && userUpdateDto.Name == null)
            {
                return BadRequest("Please provide email or name to update");
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
    }
}
