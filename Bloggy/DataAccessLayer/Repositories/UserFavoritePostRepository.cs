using Bloggy.DataAccessLayer.Models;
using Bloggy.DataAccessLayer.Repositories.IRepositories;

namespace Bloggy.DataAccessLayer.Repositories
{
    public class UserFavoritePostRepository : IUserFavoritePostRepository
    {
        private readonly MyContext _context;

        public UserFavoritePostRepository(MyContext context)
        {
            _context = context;
        }

        public async Task AddFavoritePost(UserFavoritePost favoritePost)
        {
            await _context.UserFavoritePosts.AddAsync(favoritePost);
        }

        public async Task<UserFavoritePost> GetFavoritePost(int postId, string userId)
        {
            return await _context.UserFavoritePosts.FirstOrDefaultAsync(f => f.PostId == postId && f.UserId == userId);
        }

        public async Task RemoveFavoritePost(int postId, string userId)
        {
            var favPost = await GetFavoritePost(postId, userId);

            if (favPost == null)
                return;

            _context.UserFavoritePosts.Remove(favPost);
        }
    }
}
