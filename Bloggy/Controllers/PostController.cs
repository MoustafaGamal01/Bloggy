using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bloggy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
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
        public async Task<IActionResult> AddPost(PostAddDto postDto)
        {
            if (ModelState.IsValid)
            {
                bool? ok = await _postService.AddPost(postDto);
                if (ok == true)
                    return Ok("Post Added");
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdatePost(int id, PostUpdateDto updateDto)
        {
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
            bool? ok = await _postService.DeletePost(id);
            if(ok == true)
                return Ok($"Post with id {id} has been deleted");
            return BadRequest($"Can't find post with id {id}");
        }
    }
}
