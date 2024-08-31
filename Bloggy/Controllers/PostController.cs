using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bloggy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly UserManager<ApplicationUser> _userManager;

        public PostController(IPostService postService,
            UserManager<ApplicationUser> userManager)
        {
            _postService = postService;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _postService.GetPosts();

            return Ok(posts);
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var post = await _postService.GetPostById(id);
            if (post == null)
            {
                return NotFound($"Can't find post with id {id}");
            }
            return Ok(post);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddPost([FromForm] PostAddDto postDto)
        {
            if (ModelState.IsValid)
            {
                if (postDto.Img != null && postDto.Img.Length > 2 * 1024 * 1024)
                {
                    return BadRequest("File size exceeds the maximum limit of 2MB.");
                }
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                bool? ok = await _postService.AddPost(postDto, userId);
                if (ok == true)
                    return Ok("Post Added");
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdatePost(int id, PostUpdateDto updateDto)
        {
            var userId = User.Claims.FirstOrDefault(c=>c.Type == ClaimTypes.NameIdentifier).Value;

            bool isOwner = await _postService.CheckPostOwner(id, userId);

            if (!isOwner && !User.IsInRole("Admin"))
                return Unauthorized("You are not allowed to update this post");

            if (updateDto.Content != null || updateDto.Title != null || updateDto.TimeToRead != 0 || updateDto.CategoryId != null)
            {
                bool? ok = await _postService.UpdatePost(id, updateDto);
                
                if (ok == true)
                    return Ok("Post Updated");
            }
            return BadRequest("Not Valid Data");
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // handle that only and admin or the post owner can delete the post
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            bool isOwner = await _postService.CheckPostOwner(id, userId);

            if (!isOwner && !User.IsInRole("Admin"))
                return Unauthorized("You are not allowed to update this post");

            bool? ok = await _postService.DeletePost(id);
            if(ok == true)
                return Ok($"Post with id {id} has been deleted");
            return BadRequest($"Can't find post with id {id}");
        }

        [HttpGet]
        [Route("getByCategory/{categoryId}")]
        public async Task<IActionResult> GetPostsByCategory(int categoryId)
        {
            var posts = await _postService.GetPostsByCategoryId(categoryId);
            if (posts.Count() == 0)
            {
                return NotFound($"Can't find posts with category id {categoryId}");
            }
            return Ok(posts);
        }

        [HttpGet]
        [Route("getByCategoryName/{categoryName}")]
        public async Task<IActionResult> GetPostsByCategoryName(string categoryName)
        {
            var posts = await _postService.GetPostsByCategoryName(categoryName);
            if (posts.Count() == 0)
            {
                return NotFound($"Can't find posts with category name {categoryName}");
            }
            return Ok(posts);
        }

        [HttpGet]
        [Route("search/{search}")]
        public async Task<IActionResult> SearchPosts(string search)
        {
            var posts = await _postService.SearchPosts(search);
            if (posts.Count() == 0)
            {
                return NotFound($"Can't find posts with search {search}");
            }
            return Ok(posts);
        }

        [HttpGet]
        [Route("getByUser/{userId}")]
        public async Task<IActionResult> GetPostsByUser(string userId)
        {
            var posts = await _postService.GetPostsByUserId(userId);
            if (posts.Count() == 0)
            {
                return NotFound($"Can't find posts with user id {userId}");
            }
            return Ok(posts);
        }
    }
}
