namespace Bloggy.IRepositories
{
    public interface ICategoryRepository
    {
        Task UpdateCategory(int categoryId, Category category);
        Task AddCategory(Category category);
        Task DeleteCategory(int id);
        Task<Category> GetCategoryById(int id);
        Task<IEnumerable<Category>> GetCategories();
    }
}
