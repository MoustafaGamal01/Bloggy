namespace Bloggy.Services.IServices
{
    public interface IPostService
    {
        Task<IEnumerable<PostShowDto>> GetPosts();
        Task<PostShowDto> GetPostById(int id);
        Task<bool?> AddPost(PostAddDto post);
        Task<bool?> UpdatePost(int id, PostUpdateDto post);
        Task<bool?> DeletePost(int id);
    }
}
