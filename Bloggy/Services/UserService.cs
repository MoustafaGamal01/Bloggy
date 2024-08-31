namespace Bloggy.Services
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
            // get all users in UserManager
            return await _userManager.Users.ToListAsync();
        }
    }
}
