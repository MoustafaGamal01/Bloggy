using Bloggy.BussinessLogicLayer.DTOs.CategoryDto;
using Bloggy.DataAccessLayer.Models;

namespace Bloggy.DataAccessLayer.Repositories.IRepositories
{
    public interface ICategoryRepository
    {
        Task<bool?> UpdateCategory(int categoryId, CategoryUpdateDto category);
        Task AddCategory(Category category);
        Task<bool?> DeleteCategory(int id);
        Task<Category> GetCategoryById(int id);
        Task<IEnumerable<Category>> GetCategories();
    }
}
