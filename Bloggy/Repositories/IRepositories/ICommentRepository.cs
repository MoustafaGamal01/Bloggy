namespace Bloggy.Repositories.IRepositories
{
    public interface ICommentRepository
    {
        Task AddComment(Comment comment);
        Task UpdateComment(int commentId, Comment comment);
        Task DeleteComment(int id);
        Task<Comment> GetCommentById(int id);
        Task<IEnumerable<Comment>> GetComments();
        Task<IEnumerable<Comment>> GetCommentsByPostId(int postId);
        Task<IEnumerable<Comment>> GetCommentsByUserId(string userId);
    }
}
