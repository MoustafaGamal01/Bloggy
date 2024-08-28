
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
                TimeToRead = p.TimeToRead
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
                TimeToRead = post.TimeToRead
            };
            return postDto;
        }

        public async Task<bool?> AddPost(PostAddDto postDto)
        {
            var post = new Post
            {
                Title = postDto.Title,
                Content = postDto.Content,
                Image = postDto.Img,
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
            await _unitOfWork.PostRepo.DeletePost(id);
            return await _unitOfWork.CompleteAsync() > 0;
        }
    }
}
