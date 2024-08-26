
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

        public async Task DeleteCategory(int id)
        {
            var category = await GetCategoryById(id);
            _context.Categories.Remove(category);   
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync(); 
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task UpdateCategory(int categoryId, Category category)
        {
            var cat = await GetCategoryById(categoryId);

            cat.Name = category.Name;
            cat.Description = category.Description;

            _context.Categories.Update(cat);
        }
    }
}
