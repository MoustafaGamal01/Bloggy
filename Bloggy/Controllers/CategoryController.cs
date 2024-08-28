namespace Bloggy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> Get()
        {
            var categories = await _categoryService.GetCategories();
            var catDto = categories.Select(c => new CategoryModel
            {
                Name = c.Name,
                Description = c.Description
            });

            return Ok(catDto);
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var cat = await _categoryService.GetCategory(id);

            if (cat == null) return BadRequest($"Can't find category with id {id}");
            
            return Ok(cat);
        }

        [HttpPost]
        [Route("add")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCategory([FromBody] CategoryModel categoryDto)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.AddCategory(categoryDto);
                return Ok("Category Added");
            }
            ModelState.AddModelError("", "Can't add category");
            return BadRequest(ModelState);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryUpdateModel categoryDto)
        {
            bool? ok = await _categoryService.UpdateCategory(id, categoryDto);
            
            if (ok == true)
            {
                return Ok("Category Updated");
            }
            
            ModelState.AddModelError("", "Can't update category");
            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            bool? ok = await _categoryService.DeleteCategory(id);
            if(ok == true) return Ok("Category Deleted");
            return BadRequest($"Can't find category with id {id}");
        }

    }
}
