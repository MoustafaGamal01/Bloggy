
using Bloggy.Models;
using Bloggy.Services.IServices;

namespace Bloggy.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool?> AddCategory(CategoryModel categoryModel)
        {
            var category = new Category
            {
                Name = categoryModel.Name,
                Description = categoryModel.Description,
            };
            await _unitOfWork.CategoryRepo.AddCategory(category);

            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool?> DeleteCategory(int id)
        {
            bool? ok = await _unitOfWork.CategoryRepo.DeleteCategory(id);
            if (ok == true)
            {
                return await _unitOfWork.CompleteAsync() > 0;
            }
            return false;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _unitOfWork.CategoryRepo.GetCategories();
        }

        public async Task<Category> GetCategory(int id)
        {
            return await _unitOfWork.CategoryRepo.GetCategoryById(id);   
        }

        public async Task<bool?> UpdateCategory(int id, CategoryUpdateModel categoryModel)
        {
            if (categoryModel.Name == null && categoryModel.Description == null) return false;

            bool? ok = await _unitOfWork.CategoryRepo.UpdateCategory(id, categoryModel);
            
            if (ok == true) { 
                return await _unitOfWork.CompleteAsync() > 0;
            }
            
            return false;
        }
    }
}
