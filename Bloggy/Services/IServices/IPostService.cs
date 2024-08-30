namespace Bloggy.Services.IServices
{
    public interface IPostService
    {
        Task<IEnumerable<PostShowDto>> GetPosts();
        Task<PostShowDto> GetPostById(int id);
        Task<bool?> AddPost(PostAddDto post, string userId);
        Task<bool?> UpdatePost(int id, PostUpdateDto post);
        Task<bool?> DeletePost(int id);
        Task<IEnumerable<PostShowDto>> GetPostsByCategoryId(int categoryId);
        Task<IEnumerable<PostShowDto>> GetPostsByCategoryName(string categoryName);
        Task<IEnumerable<PostShowDto>> SearchPosts(string search);
        Task<IEnumerable<PostShowDto>> GetPostsByUserId(string userId);
    }
}
