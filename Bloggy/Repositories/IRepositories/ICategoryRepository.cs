namespace Bloggy.Repositories.IRepositories
{
    public interface ICategoryRepository
    {
        Task<bool?> UpdateCategory(int categoryId, CategoryUpdateModel category);
        Task AddCategory(Category category);
        Task<bool?> DeleteCategory(int id);
        Task<Category> GetCategoryById(int id);
        Task<IEnumerable<Category>> GetCategories();
    }
}
