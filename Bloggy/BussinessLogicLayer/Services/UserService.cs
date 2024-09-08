using Bloggy.BussinessLogicLayer.DTOs.UserDto;
using Bloggy.BussinessLogicLayer.Services.IServices;
using Bloggy.DataAccessLayer.Models;
using Bloggy.DataAccessLayer.Repositories.IRepositories;

namespace Bloggy.BussinessLogicLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<ApplicationUser?> GetUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IList<string>> GetUserRoles(ApplicationUser applicationUser)
        {
            return await _userManager.GetRolesAsync(applicationUser);
        }

        public async Task<IList<ApplicationUser>> GetAllUsers()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<IdentityResult> UpdateUser(ApplicationUser user)
        {
            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> AddUser(ApplicationUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> AddUserToRole(ApplicationUser user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<IdentityResult> ChangeUserPassword(ApplicationUser user, UserChangePasswordDto userChangePasswordDto)
        {
            return await _userManager.ChangePasswordAsync(user, userChangePasswordDto.CurrentPassword, userChangePasswordDto.NewPassword);
        }

        public async Task<IList<ApplicationUser>> GetBannedUsers()
        {
            return await _userManager.Users.Where(u => u.isBanned == true).ToListAsync();
        }
    }
}
