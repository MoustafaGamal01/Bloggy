namespace Bloggy.IRepositories
{
    public interface IPostRepository
    {
        Task AddPost(Post post);
        Task UpdatePost(int postId, Post post);
        Task DeletePost(int id);
        Task<Post> GetPostById(int id);
        Task<IEnumerable<Post>> GetPosts();
        Task<IEnumerable<Post>> GetPostsByCategoryId(int categoryId);
        Task<IEnumerable<Post>> GetPostsByCategoryName(string categoryName);
        Task<IEnumerable<Post>> SearchPosts(string search);
        Task<IEnumerable<Post>> GetPostsByUserId(string UserId);
    }
}
