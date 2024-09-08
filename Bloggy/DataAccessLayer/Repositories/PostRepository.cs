using Bloggy.BussinessLogicLayer.DTOs.PostDto;
using Bloggy.DataAccessLayer.Models;
using Bloggy.DataAccessLayer.Repositories.IRepositories;
using Microsoft.Extensions.Hosting;

namespace Bloggy.DataAccessLayer.Repositories
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
        private int pageSize = 10;

        public async Task<PagedResult<Post>> GetPosts(int pageNumber)
        {
            var totalItems = await _context.Posts.CountAsync();
            var posts = await _context.Posts
                .Include(p => p.Comments)
                .Include(p => p.Category)
                .Include(p => p.User)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Post>
            {
                Items = posts,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPosts = totalItems
            };
        }

        public async Task<Post> GetPostById(int id)
        {
            return await _context.Posts
                .Include(p => p.Comments)

                .Include(p => p.Category)
                .Include(p => p.User)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task UpdatePost(int postId, PostUpdateDto postDto)
        {
            var post = await GetPostById(postId);

            if (postDto.Title != null) post.Title = postDto.Title;
            if (postDto.Content != null) post.Content = postDto.Content;
            if (postDto.CategoryId != null) post.CategoryId = postDto.CategoryId;
            if (postDto.TimeToRead != null) post.TimeToRead = postDto.TimeToRead;

            _context.Posts.Update(post);
        }

        public async Task<PagedResult<Post>> GetPostsByCategoryId(int categoryId, int pageNumber)
        {
            var totalItems = await _context.Posts
                .Include(c => c.Category)
                .Where(c => c.CategoryId == categoryId)
                .CountAsync();

            var posts = await _context.Posts
                .Include(p => p.Comments)
                .Include(p => p.Category)
                .Include(p => p.User)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Where(c => c.CategoryId == categoryId)
                .ToListAsync();

            return new PagedResult<Post>
            {
                Items = posts,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPosts = totalItems
            };
        }

        public async Task<PagedResult<Post>> GetPostsByCategoryName(string categoryName, int pageNumber)
        {
            var totalItems = await _context.Posts
                .Include(c => c.Category)
                .Where(c => c.Category.Name.Contains(categoryName))
                .CountAsync();

            var posts = await _context.Posts
                .Include(p => p.Comments)
                .Include(p => p.Category)
                .Include(p => p.User)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Where(c => c.Category.Name.Contains(categoryName))
                .ToListAsync();

            return new PagedResult<Post>
            {
                Items = posts,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPosts = totalItems
            };
        }

        public async Task<PagedResult<Post>> SearchPosts(string search, int pageNumber)
        {
            var posts = await _context.Posts
                .Include(p => p.Comments)
                .Include(i => i.Category)
                .Include(i => i.User)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Where(i => i.Title.Contains(search) || i.Content.Contains(search)
                || i.Category.Name.Contains(search) || i.User.UserName.Contains(search))
                .ToListAsync();

            return new PagedResult<Post>
            {
                Items = posts,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPosts = posts.Count
            };
        }

        public async Task<PagedResult<Post>> GetPostsByUserId(string UserId, int pageNumber)
        {
            var posts = await _context.Posts
                .Include(p => p.Comments)
                .Include(p => p.Category)
                .Include(p => p.User)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Where(i => i.UserId == UserId)
                .ToListAsync();

            return new PagedResult<Post>
            {
                Items = posts,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPosts = posts.Count
            };
        }

        public async Task<PagedResult<Post>> GetFavoritePostsByUserId(string UserId, int pageNumber)
        {
            var posts = await _context.UserFavoritePosts
                .Include(p => p.Post)
                .ThenInclude(p => p.Comments)
                .Include(p => p.Post)
                .ThenInclude(p => p.Category)
                .Include(p => p.Post)
                .ThenInclude(p => p.User)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Where(i => i.UserId == UserId)
                .Select(i => i.Post)
                .ToListAsync();

            return new PagedResult<Post>
            {
                Items = posts,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPosts = posts.Count
            };
        }

        public async Task<int> PostsCount()
        {
            return await _context.Posts.CountAsync();
        }
    }
}
