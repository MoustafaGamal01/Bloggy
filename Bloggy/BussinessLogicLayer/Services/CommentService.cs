﻿using Bloggy.BussinessLogicLayer.DTOs.CommentDto;
using Bloggy.BussinessLogicLayer.Services.IServices;
using Bloggy.DataAccessLayer.Models;
using Bloggy.DataAccessLayer.Repositories.IRepositories;

namespace Bloggy.BussinessLogicLayer.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommentService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> _userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = _userManager;
        }

        private IEnumerable<CommentShowDto> FromCommenttoListDto(IEnumerable<Comment> comments)
        {
            return comments.Select(c => new CommentShowDto
            {
                Content = c.Content,
                CreatedAt = c.CreatedAt,
                Username = c.User.DisplayName,
                Img = c.User.ProfilePicture
            });
        }
        public async Task<bool?> AddComment(CommentAddDto commentDto, string userId)
        {
            var comment = new Comment
            {
                Content = commentDto.Content,
                UserId = userId,
                PostId = commentDto.PostId,
                CreatedAt = DateTime.Now,
            };
            await _unitOfWork.CommentRepo.AddComment(comment);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool?> DeleteComment(int id)
        {
            await _unitOfWork.CommentRepo.DeleteComment(id);

            return await _unitOfWork.CompleteAsync() > 0;
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
                Username = comment.User.DisplayName,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
                Img = comment.User.ProfilePicture
            };

            return cmntDto;
        }

        public async Task<IEnumerable<CommentShowDto>> GetCommentsByPostId(int postId)
        {
            var comments = await _unitOfWork.CommentRepo.GetCommentsByPostId(postId);

            return FromCommenttoListDto(comments);
        }

        public async Task<IEnumerable<CommentShowDto>> GetCommentsByUserId(string userId)
        {
            var comments = await _unitOfWork.CommentRepo.GetCommentsByUserId(userId);

            return FromCommenttoListDto(comments);
        }

        public async Task<bool?> UpdateComment(int commentId, CommentUpdateDto commentDto)
        {
            await _unitOfWork.CommentRepo.UpdateComment(commentId, commentDto);

            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool> CheckCommentOwner(int id, string userId)
        {
            var comment = await _unitOfWork.CommentRepo.GetCommentById(id);

            return comment.UserId == userId;
        }
    }
}
