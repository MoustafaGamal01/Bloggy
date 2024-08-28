﻿namespace Bloggy.Repositories.IRepositories
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepo { get; }
        IPostRepository PostRepo { get; }
        ICommentRepository CommentRepo { get; }
        //IUserRepository User { get; }
        Task<int> CompleteAsync();
    }
}
