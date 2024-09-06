namespace Bloggy.Services.IServices
{
    public interface IPostService
    {
        Task<PagedResult<PostShowDto>> GetPosts(int pageNumber);
        Task<PostShowDto> GetPostById(int id);
        Task<bool?> AddPost(PostAddDto post, string userId);
        Task<bool?> UpdatePost(int id, PostUpdateDto post);
        Task<bool?> DeletePost(int id);
        Task<PagedResult<PostShowDto>> GetPostsByCategoryId(int categoryId, int pageNumber);
        Task<PagedResult<PostShowDto>> GetPostsByCategoryName(string categoryName, int pageNumber);
        Task<PagedResult<PostShowDto>> SearchPosts(string search, int pageNumber);
        Task<PagedResult<PostShowDto>> GetPostsByUserId(string userId, int pageNumber);
        Task<PagedResult<PostShowDto>> GetFavoritePostsByUserId(string userId, int pageNumber);
        Task<bool> CheckPostOwner(int postId, string userId);
        Task<bool> ManagePostFavoriteStatus(int postId, string userId);
    }
}
