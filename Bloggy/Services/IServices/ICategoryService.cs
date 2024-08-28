using Bloggy.DTOs.CategoryDto;

namespace Bloggy.Services.IServices
{
    public interface ICategoryService
    {
        Task<bool?> AddCategory(CategoryAddDto categoryModel);
        Task<IEnumerable<Category>> GetCategories();
        Task<CategoryAddDto> GetCategoryById(int id);
        Task<bool?> UpdateCategory(int id, CategoryUpdateDto categoryModel);
        Task<bool?> DeleteCategory(int id);
    }
}
