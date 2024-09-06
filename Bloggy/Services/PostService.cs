
using Bloggy.DTOs.PostDto;
using Bloggy.Models;
using Microsoft.EntityFrameworkCore;

namespace Bloggy.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PostService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        private PagedResult<PostShowDto> FromPostToListDto(int totalCount, PagedResult<Post> posts)
        {
            return _mapper.Map<PagedResult<PostShowDto>>(new PagedResult<Post>
            {
                Items = posts.Items,
                PageNumber = posts.PageNumber,
                PageSize = posts.PageSize,
                TotalPosts = posts.TotalPosts
            });
        }

        public async Task<PagedResult<PostShowDto>> GetPosts(int pageNumber)
        {
            var posts = await _unitOfWork.PostRepo.GetPosts(pageNumber);

            return FromPostToListDto(posts.TotalPosts, posts);
        }

        public async Task<PostShowDto> GetPostById(int id)
        {
            var post = await _unitOfWork.PostRepo.GetPostById(id);
            if (post == null)
            {
                return null;
            }

            PostShowDto postShow = new PostShowDto();
            List<CommentShowDto> cmntsDto = new List<CommentShowDto>();

            postShow.Id = post.Id;
            postShow.Title = post.Title;
            postShow.Content = post.Content;
            postShow.Category = post.Category.Name;
            postShow.CreatedAt = post.CreatedAt;
            postShow.UserName = post.User.DisplayName;
            postShow.Img = post.Image;
            postShow.TimeToRead = post.TimeToRead;
            foreach(var comment in post.Comments)
            {
                var cmntDto = new CommentShowDto
                {
                    Content = comment.Content,
                    CreatedAt = comment.CreatedAt,
                    Username = comment.User.DisplayName,
                    Img = comment.User.ProfilePicture
                };
                cmntsDto.Add(cmntDto);
            }
            postShow.Comments = cmntsDto;
            return postShow;
        }

        public async Task<bool?> AddPost(PostAddDto postDto, string userId)
        {
            byte[]? imageData = null;
            if (postDto.Img != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await postDto.Img.CopyToAsync(memoryStream);
                    imageData = memoryStream.ToArray();  
                }
            }

            var post = new Post
            {
                Title = postDto.Title,
                Content = postDto.Content,
                Image = imageData,
                TimeToRead = postDto.TimeToRead,
                CreatedAt = DateTime.Now,
                CategoryId = postDto.CategoryId,
                UserId = userId
            };

            await _unitOfWork.PostRepo.AddPost(post);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool?> UpdatePost(int id, PostUpdateDto post)
        {
            await _unitOfWork.PostRepo.UpdatePost(id, post);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool?> DeletePost(int id)
        {
            var comments = await _unitOfWork.CommentRepo.GetCommentsByPostId(id);
            foreach (var comment in comments)
            {
                await _unitOfWork.CommentRepo.DeleteComment(comment.Id);
            }
            await _unitOfWork.PostRepo.DeletePost(id);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<PagedResult<PostShowDto>> GetPostsByCategoryId(int categoryId, int pageNumber)
        {
            var posts = await _unitOfWork.PostRepo.GetPostsByCategoryId(categoryId, pageNumber);
           
            return FromPostToListDto(posts.TotalPosts, posts);
        }

        public async Task<PagedResult<PostShowDto>> GetPostsByCategoryName(string categoryName, int pageNumber)
        {
            var posts = await _unitOfWork.PostRepo.GetPostsByCategoryName(categoryName, pageNumber);

            return FromPostToListDto(posts.TotalPosts, posts);
        }

        public async Task<PagedResult<PostShowDto>> SearchPosts(string search, int pageNumber)
        {
            var posts = await _unitOfWork.PostRepo.SearchPosts(search, pageNumber);

            return FromPostToListDto(posts.TotalPosts, posts);
        }

        public async Task<PagedResult<PostShowDto>> GetPostsByUserId(string userId, int pageNumber)
        {
            var posts = await _unitOfWork.PostRepo.GetPostsByUserId(userId, pageNumber);

            return FromPostToListDto(posts.TotalPosts, posts);
        }

        public async Task<bool> CheckPostOwner(int postId, string userId)
        {
            var post = await _unitOfWork.PostRepo.GetPostById(postId);

            return post.UserId == userId;
        }

        public async Task<PagedResult<PostShowDto>> GetFavoritePostsByUserId(string userId, int pageNumber)
        {
            var posts = await _unitOfWork.PostRepo.GetFavoritePostsByUserId(userId, pageNumber);

            return FromPostToListDto(posts.TotalPosts, posts);
        }

        public async Task<bool> ManagePostFavoriteStatus(int postId, string userId)
        {
            var favPost = await _unitOfWork.UserFavoritePostRepo.GetFavoritePost(postId, userId);

            if (favPost == null)
            {
                var favoritePost = new UserFavoritePost
                {
                    PostId = postId,
                    UserId = userId
                };

                await _unitOfWork.UserFavoritePostRepo.AddFavoritePost(favoritePost);
            }
            else
            {
                await _unitOfWork.UserFavoritePostRepo.RemoveFavoritePost(postId, userId);
            }

            return await _unitOfWork.CompleteAsync() > 0;
        }
    }
}
