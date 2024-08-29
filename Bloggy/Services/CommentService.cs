
using Bloggy.Models;

namespace Bloggy.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommentShowDto> GetCommentById(int id)
        {
            var comment = await _unitOfWork.CommentRepo.GetCommentById(id);
            if (comment == null)
            {
                return null;
            }
            var cmntDto = new CommentShowDto
            {
                //Username = comment.User.UserName,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt
            };

            return cmntDto;
        }

        public async Task<IEnumerable<CommentShowDto>> GetCommentsByPostId(int postId)
        {
            var comments = await _unitOfWork.CommentRepo.GetCommentsByPostId(postId);

            var cmntsDto = comments.Select(c => new CommentShowDto 
            {
                Content = c.Content,
                CreatedAt = c.CreatedAt,
                // Username = c.User.UserName
            });

            return cmntsDto;
        }

        public async Task<IEnumerable<CommentShowDto>> GetCommentsByUserId(string userId)
        {
            var comments = await _unitOfWork.CommentRepo.GetCommentsByUserId(userId);

            var cmntsDto = comments.Select(c => new CommentShowDto
            {
                Content = c.Content,
                CreatedAt = c.CreatedAt,
                // Username = c.User.UserName
            });

            return cmntsDto;
        }
    }
}
