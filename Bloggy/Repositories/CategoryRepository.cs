
using Bloggy.DTOs.CategoryDto;
using Bloggy.Repositories.IRepositories;

namespace Bloggy.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MyContext _context;

        public CategoryRepository(MyContext context)
        {
            _context = context;
        }

        public async Task AddCategory(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public async Task<bool?> DeleteCategory(int id)
        {
            var category = await GetCategoryById(id);
            
            if(category == null) return false;

            _context.Categories.Remove(category);
            
            return true;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<bool?> UpdateCategory(int categoryId, CategoryUpdateDto category)
        {
            var cat = await GetCategoryById(categoryId);

            if (cat == null) return false;

            if(category.Name != null) cat.Name = category.Name;
            if (category.Description != null) cat.Description = category.Description;

            _context.Categories.Update(cat);
            return true;
        }
    }
}
