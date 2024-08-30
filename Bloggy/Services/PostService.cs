
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

        public async Task<IEnumerable<PostShowDto>> GetPosts()
        {
            var posts =  await _unitOfWork.PostRepo.GetPosts();

            var postsDto = posts.Select(p => new PostShowDto
            {
                Title = p.Title,
                Content = p.Content,
                Category = p.Category.Name,
                CreatedAt = p.CreatedAt,
                Img = p.Image,
                //UserName = p.User.UserName,
                TimeToRead = p.TimeToRead,
                Comments = p.Comments,
            });

            return postsDto;
        }

        public async Task<PostShowDto> GetPostById(int id)
        {
            var post = await _unitOfWork.PostRepo.GetPostById(id);
            if (post == null)
            {
                return null;
            }
            var postDto = new PostShowDto
            {
                Title = post.Title,
                Content = post.Content,
                Category = post.Category.Name,
                CreatedAt = post.CreatedAt,
                Img = post.Image,
                //UserName = post.User.UserName,
                TimeToRead = post.TimeToRead,
                Comments = post.Comments,
            };
            return postDto;
        }

        public async Task<bool?> AddPost(PostAddDto postDto)
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
                CategoryId = postDto.CategoryId
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
        
            var postsDto = posts.Select(p => new PostShowDto {
                Title = p.Title,
                Content = p.Content,
                Category = p.Category.Name,
                CreatedAt = p.CreatedAt,
                Img = p.Image,
                //UserName = p.User.UserName,
                TimeToRead = p.TimeToRead,
                Comments = p.Comments,
            });

            return postsDto;
        }

        public async Task<IEnumerable<PostShowDto>> GetPostsByCategoryName(string categoryName)
        {
            var posts = await _unitOfWork.PostRepo.GetPostsByCategoryName(categoryName);

            var postsDto = posts.Select(p => new PostShowDto
            {
                Title = p.Title,
                Content = p.Content,
                Category = p.Category.Name,
                CreatedAt = p.CreatedAt,
                Img = p.Image,
                //UserName = p.User.UserName,
                TimeToRead = p.TimeToRead,
                Comments = p.Comments,
            });

            return postsDto;
        }

        public async Task<IEnumerable<PostShowDto>> SearchPosts(string search)
        {
            var posts = await _unitOfWork.PostRepo.SearchPosts(search);

            var postsDto = posts.Select(p => new PostShowDto
            {
                Title = p.Title,
                Content = p.Content,
                Category = p.Category.Name,
                CreatedAt = p.CreatedAt,
                Img = p.Image,
                //UserName = p.User.UserName,
                TimeToRead = p.TimeToRead,
                Comments = p.Comments,
            });

            return postsDto;
        }

        public async Task<IEnumerable<PostShowDto>> GetPostsByUserId(string userId)
        {
            var posts = await _unitOfWork.PostRepo.GetPostsByUserId(userId);

            var postsDto = posts.Select(p => new PostShowDto
            {
                Title = p.Title,
                Content = p.Content,
                Category = p.Category.Name,
                CreatedAt = p.CreatedAt,
                Img = p.Image,
                //UserName = p.User.UserName,
                TimeToRead = p.TimeToRead,
                Comments = p.Comments,
            });

            return postsDto;
        }
    }
}
