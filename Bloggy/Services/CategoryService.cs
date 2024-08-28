
using Bloggy.DTOs.CategoryDto;
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

        public async Task<bool?> AddCategory(CategoryAddDto categoryModel)
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
            var categories = await _unitOfWork.CategoryRepo.GetCategories();
            
            return categories;
        }

        public async Task<CategoryAddDto> GetCategoryById(int id)
        {
            var category =  await _unitOfWork.CategoryRepo.GetCategoryById(id);   
        
            var categoryDto = new CategoryAddDto
            {
                Name = category.Name,
                Description = category.Description
            };
            return categoryDto;
        }

        public async Task<bool?> UpdateCategory(int id, CategoryUpdateDto categoryModel)
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
