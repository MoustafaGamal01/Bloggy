namespace Bloggy.Services.IServices
{
    public interface ICategoryService
    {
        Task<bool?> AddCategory(CategoryModel categoryModel);
        Task<IEnumerable<Category>> GetCategories();
        //Task<CategoryModel> GetCategory(int id);
        Task<bool?> UpdateCategory(int id, CategoryUpdateModel categoryModel);
        Task<bool?> DeleteCategory(int id);
    }
}
