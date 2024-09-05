namespace Bloggy.Models
{
    public class MyContext : IdentityDbContext<ApplicationUser>
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }
      
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserFavoritePost> UserFavoritePosts { get; set; }
        //public ApplicationUser ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Category
            modelBuilder.Entity<Category>()
                .Property(c => c.Name)
                .IsUnicode(true);

            modelBuilder.Entity<Category>()
                .Property(c => c.Description)
                .IsUnicode(true);

            // Comment
            modelBuilder.Entity<Comment>()
                .Property(c => c.Content)
                .IsUnicode(true);

            // Post
            modelBuilder.Entity<Post>()
                .Property(p => p.Title)
                .IsUnicode(true);

            modelBuilder.Entity<Post>()
                .Property(p => p.Content)
                .IsUnicode(true);
        }
    }
}
