namespace Bloggy.Services.IServices
{
    public interface IUserService
    {
        Task<ApplicationUser?> GetUserByEmail(string email);
        Task<IList<string>> GetUserRoles(ApplicationUser applicationUser);
        Task<IList<ApplicationUser>> GetAllUsers();

    }
}
