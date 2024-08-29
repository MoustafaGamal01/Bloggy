namespace Bloggy.Services.IServices
{
    public interface ICommentService
    {
        Task<CommentShowDto> GetCommentById(int id);
        Task<IEnumerable<CommentShowDto>> GetCommentsByPostId(int postId);
        Task<IEnumerable<CommentShowDto>> GetCommentsByUserId(string userId);
        //Task<Comment> AddComment(Comment comment);
        //Task<Comment> UpdateComment(Comment comment);
        //Task<Comment> DeleteComment(int id);
    }
}
