namespace Bloggy.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyContext _context;

        public ICategoryRepository CategoryRepo { get; private set; }
        public IPostRepository PostRepo { get; private set; }
        public ICommentRepository CommentRepo { get; private set; }

        public UnitOfWork(MyContext context,
                          ICategoryRepository categoryRepository,
                          IPostRepository postRepository,
                          ICommentRepository commentRepository)
        {
            _context = context;
            CategoryRepo = categoryRepository;
            PostRepo = postRepository;
            CommentRepo = commentRepository;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync(); 
        }

        public void Dispose()
        {
            _context.Dispose(); 
        }
    }

}
