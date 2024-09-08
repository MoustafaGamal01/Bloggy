using Bloggy.BussinessLogicLayer.DTOs.CommentDto;
using Bloggy.DataAccessLayer.Models;

namespace Bloggy.DataAccessLayer.Repositories.IRepositories
{
    public interface ICommentRepository
    {
        Task AddComment(Comment comment);
        Task UpdateComment(int commentId, CommentUpdateDto comment);
        Task DeleteComment(int id);
        Task<Comment> GetCommentById(int id);
        Task<IEnumerable<Comment>> GetComments();
        Task<IEnumerable<Comment>> GetCommentsByPostId(int postId);
        Task<IEnumerable<Comment>> GetCommentsByUserId(string userId);
    }
}
