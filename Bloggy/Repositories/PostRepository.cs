﻿using Bloggy.Repositories.IRepositories;

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
            return await _context.
                Posts
                .Include(p => p.Comments)
                .Include(p => p.Category)
                .Include(p=>p.User)
                .ToListAsync();
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

            if(postDto.Title != null) post.Title = postDto.Title;
            if (postDto.Content != null) post.Content = postDto.Content;
            if (postDto.CategoryId != null) post.CategoryId = postDto.CategoryId;
            if (postDto.TimeToRead != null) post.TimeToRead = postDto.TimeToRead;

            _context.Posts.Update(post);
        }

        public async Task<IEnumerable<Post>> GetPostsByCategoryId(int categoryId)
        {
            return await _context.Posts
                .Include(p => p.Comments)
                .Include(i => i.Category)
                .Include(p => p.User)
                .Where(i => i.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsByCategoryName(string categoryName)
        {
            return await _context.Posts
                .Include(p=>p.Category)
                .Include(p => p.User)
                .Include(p => p.Comments)
                .Where(i => i.Category.Name.Contains(categoryName))
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> SearchPosts(string search)
        {
            return await _context.Posts
                .Include(p => p.Comments)
                .Include(i=>i.Category)
                .Include(i=>i.User)
                .Where(i => i.Title.Contains(search) || i.Content.Contains(search) 
                || i.Category.Name.Contains(search) || i.User.UserName.Contains(search))
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsByUserId(string UserId)
        {
            return await _context.Posts
                .Include(p => p.Comments)
                .Include(p => p.Category)
                .Include(p => p.User)
                .Where(i => i.UserId == UserId)
                .ToListAsync();
        }

    }
}
