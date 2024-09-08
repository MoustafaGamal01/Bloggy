namespace Bloggy.DataAccessLayer.Repositories.IRepositories
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepo { get; }
        IPostRepository PostRepo { get; }
        ICommentRepository CommentRepo { get; }
        IUserFavoritePostRepository UserFavoritePostRepo { get; }
        //IUserRepository User { get; }
        Task<int> CompleteAsync();
    }
}
