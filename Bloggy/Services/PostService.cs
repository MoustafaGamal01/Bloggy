
using Bloggy.DTOs.PostDto;
using Bloggy.Models;

namespace Bloggy.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private IEnumerable<PostShowDto> FromPostToListDto(IEnumerable<Post> posts)
        {
            PostShowDto postShow = new PostShowDto();
            List<PostShowDto> postShowDtos = new List<PostShowDto>();
            CommentShowDto cmntDto = new CommentShowDto();
            List<CommentShowDto> cmntsDto = new List<CommentShowDto>();

            foreach (var post in posts)
            {
                postShow.Id = post.Id;
                postShow.Title = post.Title;
                postShow.Content = post.Content;
                postShow.Category = post.Category.Name;
                postShow.CreatedAt = post.CreatedAt;
                postShow.UserName = post.User.DisplayName;
                postShow.Img = post.Image;
                postShow.TimeToRead = post.TimeToRead;
                foreach (var comment in post.Comments)
                {
                    cmntDto = new CommentShowDto
                    {
                        Content = comment.Content,
                        CreatedAt = comment.CreatedAt,
                        Username = comment.User.DisplayName,
                        Img = comment.User.ProfilePicture
                    };
                    cmntsDto.Add(cmntDto);
                }
                postShow.Comments = cmntsDto;
                postShowDtos.Add(postShow);
            }

            return postShowDtos;
        }

        public async Task<IEnumerable<PostShowDto>> GetPosts()
        {
            var posts =  await _unitOfWork.PostRepo.GetPosts();

            return FromPostToListDto(posts);    
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

        public async Task<IEnumerable<PostShowDto>> GetPostsByCategoryId(int categoryId)
        {
            var posts = await _unitOfWork.PostRepo.GetPostsByCategoryId(categoryId);

            return FromPostToListDto(posts);
        }

        public async Task<IEnumerable<PostShowDto>> GetPostsByCategoryName(string categoryName)
        {
            var posts = await _unitOfWork.PostRepo.GetPostsByCategoryName(categoryName);

            return FromPostToListDto(posts);
        }

        public async Task<IEnumerable<PostShowDto>> SearchPosts(string search)
        {
            var posts = await _unitOfWork.PostRepo.SearchPosts(search);

            return FromPostToListDto(posts);
        }

        public async Task<IEnumerable<PostShowDto>> GetPostsByUserId(string userId)
        {
            var posts = await _unitOfWork.PostRepo.GetPostsByUserId(userId);

            return FromPostToListDto(posts);
        }

        public async Task<bool> CheckPostOwner(int postId, string userId)
        {
            var post = await _unitOfWork.PostRepo.GetPostById(postId);

            return post.UserId == userId;
        }
    }
}
