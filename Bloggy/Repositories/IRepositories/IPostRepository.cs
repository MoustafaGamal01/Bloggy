namespace Bloggy.Repositories.IRepositories
{
    public interface IPostRepository
    {
        Task AddPost(Post post);
        Task UpdatePost(int postId, PostUpdateDto post);
        Task DeletePost(int id);
        Task<Post> GetPostById(int id);
        Task<PagedResult<Post>> GetPosts(int pageNumber);
        Task<PagedResult<Post>> GetPostsByCategoryId(int categoryId, int pageNumber);
        Task<PagedResult<Post>> GetPostsByCategoryName(string categoryName, int pageNumber);
        Task<PagedResult<Post>> SearchPosts(string search, int pageNumber);
        Task<PagedResult<Post>> GetPostsByUserId(string UserId, int pageNumber);
        Task<PagedResult<Post>> GetFavoritePostsByUserId(string UserId, int pageNumber);
        Task<int> PostsCount();
    }
}
