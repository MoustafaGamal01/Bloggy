using Bloggy.BussinessLogicLayer.DTOs.CommentDto;
using Bloggy.DataAccessLayer.Models;
using Bloggy.DataAccessLayer.Repositories.IRepositories;

namespace Bloggy.DataAccessLayer.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly MyContext _context;

        public CommentRepository(MyContext context)
        {
            _context = context;
        }

        public async Task AddComment(Comment comment)
        {
            await _context.AddAsync(comment);
        }

        public async Task DeleteComment(int id)
        {
            var comment = await GetCommentById(id);
            if (comment == null) return;
            _context.Comments.Remove(comment);
        }

        public async Task<Comment> GetCommentById(int id)
        {
            return await _context
                .Comments
                .Include(c => c.User)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Comment>> GetComments()
        {
            return await _context
                .Comments
                .Include(c => c.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostId(int postId)
        {
            return await _context.Comments
                .Include(c => c.User)
                .Where(i => i.PostId == postId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsByUserId(string userId)
        {
            return await _context
                .Comments
                .Include(c => c.User)
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        public async Task UpdateComment(int commentId, CommentUpdateDto commentDto)
        {
            var existComment = await GetCommentById(commentId);

            if (existComment == null) return;

            existComment.Content = commentDto.Content;

            _context.Comments.Update(existComment);
        }
    }
}
