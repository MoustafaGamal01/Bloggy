using Bloggy.Models;

namespace Bloggy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtOptions _jwtOptions;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(JwtOptions jwtOptions,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _jwtOptions = jwtOptions;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm] RegisterDto model)
        {
            if (ModelState.IsValid)
            {
                var isFound = await _userManager.FindByEmailAsync(model.Email);
                if (isFound != null)
                {
                    return BadRequest("Email already exists");
                }

                byte[]? Image = null;
                if (model.ProfilePicture != null)
                {
                    if (model.ProfilePicture.Length > 2 * 1024 * 1024)
                    {
                        return BadRequest("Profile picture must be 2mb max.");
                    }
                    using (var memoryStream = new MemoryStream())
                    {
                        await model.ProfilePicture.CopyToAsync(memoryStream);
                        Image = memoryStream.ToArray();
                    }

                }
                var user = new ApplicationUser
                {
                    Email = model.Email,
                    UserName = model.Email,
                    DisplayName = model.Name,
                    ProfilePicture = Image
                };

                var isCreated = _userManager.CreateAsync(user, model.Password);
                if (isCreated.Result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    await _signInManager.SignInAsync(user, true);
                    return Ok("User created successfully");
                }
            }
            return BadRequest("Enter valid data");
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    if(user.isBanned == true)
                    {
                        return BadRequest("You are banned, Contact with admin");
                    }
                    var roles = await _userManager.GetRolesAsync(user);
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Name, user.DisplayName),
                    };

                    claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Issuer = _jwtOptions.Issuer,
                        Audience = _jwtOptions.Audience,
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key)), SecurityAlgorithms.HmacSha256),
                        Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.LifeTime),
                        Subject = new ClaimsIdentity(claims)
                    };

                    var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                    var accessToken = tokenHandler.WriteToken(securityToken);
                
                    return Ok(accessToken);
                }
            }
            return BadRequest("Invalid email or password");
        }

        [HttpGet]
        [Route("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("Logged out");
        }
    }
}
