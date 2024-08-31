﻿namespace Bloggy.Services.IServices
{
    public interface IUserService
    {
        Task<ApplicationUser?> GetUserByEmail(string email);
        Task<IList<string>> GetUserRoles(ApplicationUser applicationUser);
        Task<IList<ApplicationUser>> GetAllUsers();
        Task<IdentityResult> UpdateUser(ApplicationUser user);
        Task<IdentityResult> AddUser(ApplicationUser user, string password);
        Task<IdentityResult> AddUserToRole(ApplicationUser user, string role);
    }
}
