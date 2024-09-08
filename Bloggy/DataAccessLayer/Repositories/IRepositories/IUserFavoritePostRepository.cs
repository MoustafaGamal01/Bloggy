using Bloggy.DataAccessLayer.Models;

namespace Bloggy.DataAccessLayer.Repositories.IRepositories
{
    public interface IUserFavoritePostRepository
    {
        Task<UserFavoritePost> GetFavoritePost(int postId, string userId);
        Task AddFavoritePost(UserFavoritePost favoritePost);
        Task RemoveFavoritePost(int postId, string userId);
    }
}
