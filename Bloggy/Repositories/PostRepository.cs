using Bloggy.Repositories.IRepositories;

namespace Bloggy.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly MyContext _context;

        public PostRepository(MyContext context)
        {
            _context = context;
        }

        public async Task AddPost(Post post)
        {
            await _context.Posts.AddAsync(post);
        }

        public async Task DeletePost(int id)
        {
            var post = await GetPostById(id);
            _context.Posts.Remove(post);
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await _context.Posts.Include(p => p.Category).ToListAsync();
        }

        public async Task<Post> GetPostById(int id)
        {
            return await _context.Posts
                .Include(p => p.Category)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task UpdatePost(int postId, PostUpdateDto postDto)
        {
            var post = await GetPostById(postId);

            if(postDto.Title != null) post.Title = postDto.Title;
            if (postDto.Content != null) post.Content = postDto.Content;
            if (postDto.CategoryId != null) post.CategoryId = postDto.CategoryId;
            if (postDto.TimeToRead != null) post.TimeToRead = postDto.TimeToRead;

            _context.Posts.Update(post);
        }

        public async Task<IEnumerable<Post>> GetPostsByCategoryId(int categoryId)
        {
            return await _context.Posts
                .Include(i=>i.Category)
                .Where(i => i.CategoryId == i.CategoryId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsByCategoryName(string categoryName)
        {
            return await _context
                .Posts.Include(i=>i.Category)
                .Where(i => i.Category.Name.Contains(categoryName))
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> SearchPosts(string search)
        {
            return await _context.Posts
                .Include(i=>i.Category)
                .Where(i => i.Title.Contains(search) || i.Content.Contains(search) || i.Category.Name.Contains(search))
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsByUserId(string UserId)
        {
            return await _context.Posts.Include(p => p.User).Where(i => i.UserId == UserId).ToListAsync();
        }
    }
}
