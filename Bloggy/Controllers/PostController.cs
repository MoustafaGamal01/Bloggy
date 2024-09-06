using Bloggy.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bloggy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        public async Task<IActionResult> GetAllPosts(int pageNumber = 1)
        {
            var posts = await _postService.GetPosts(pageNumber);
            if (posts.TotalPosts == 0)
            {
                return NotFound($"Can't find posts");
            }
            if(pageNumber > posts.TotalPages)
            {
                return NotFound($"Can't find more posts");
            }
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
        public async Task<IActionResult> GetPostsByCategory(int categoryId, int pageNumber = 1)
        {
            var posts = await _postService.GetPostsByCategoryId(categoryId, pageNumber);
            if (posts.TotalPosts == 0)
            {
                return NotFound($"Can't find posts for category {categoryId}");
            }
            if (pageNumber > posts.TotalPages)
            {
                return NotFound($"Can't find more posts for category {categoryId}");
            }
                return Ok(posts);
        }

        [HttpGet]
        [Route("getByCategoryName/{categoryName}")]
        public async Task<IActionResult> GetPostsByCategoryName(string categoryName, int pageNumber = 1)
        {
            var posts = await _postService.GetPostsByCategoryName(categoryName, pageNumber);
            if (posts.TotalPosts == 0)
            {
                return NotFound($"Can't find posts for category {categoryName}");
            }
            if (pageNumber > posts.TotalPages)
            {
                return NotFound($"Can't find more posts for category {categoryName}");
            }
            return Ok(posts);
        }

        [HttpGet]
        [Route("search/{search}")]
        public async Task<IActionResult> SearchPosts(string search, int pageNumber = 1)
        {
            var posts = await _postService.SearchPosts(search, pageNumber);
            if (posts.TotalPosts == 0)
            {
                return NotFound($"Can't find posts with search {search}");
            }
            if (pageNumber > posts.TotalPages)
            {
                return NotFound($"Can't find more posts with search {search}");
            }
            return Ok(posts);
        }

        [HttpGet]
        [Route("getByUser/{userId}")]
        public async Task<IActionResult> GetPostsByUser(string userId, int pageNumber = 1)
        {
            var posts = await _postService.GetPostsByUserId(userId, pageNumber);
            if (posts.TotalPosts == 0)
            {
                return NotFound($"Can't find posts for user {userId}");
            }
            if (pageNumber > posts.TotalPages)
            {
                return NotFound($"Can't find posts for more user {userId}");
            }
            return Ok(posts);
        }

        [HttpGet]
        [Route("getFavoritePosts")]
        public async Task<IActionResult> GetFavoritePosts(int pageNumber = 1)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var posts = await _postService.GetFavoritePostsByUserId(userId, pageNumber);
            if (posts.TotalPosts == 0)
            {
                return NotFound($"Can't find favorite posts for this user");
            }
            if (pageNumber > posts.TotalPages)
            {
                return NotFound($"Can't find more favorite posts for this user");
            }
            return Ok(posts);
        }

        [HttpPost]
        [Route("favorite/{postId}")]
        public async Task<IActionResult> ManageFavoritePost(int postId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            bool ok = await _postService.ManagePostFavoriteStatus(postId, userId);
            if (ok == true)
                return Ok("Favorite list has been updated");
            return BadRequest("Can't add this post to your favorite list");
        }
    }
}
