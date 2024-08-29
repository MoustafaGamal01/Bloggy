using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bloggy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<ActionResult<CommentShowDto>> GetCommentById(int id)
        {
            var comment = await _commentService.GetCommentById(id);

            if (comment == null)
            {
                return NotFound($"Can't find comment with id {id}");
            }

            return Ok(comment);
        }

        [HttpGet]
        [Route("getByPostId/{id}")]
        public async Task<ActionResult<IEnumerable<CommentShowDto>>> GetCommentsByPostId(int id)
        {
            var comments = await _commentService.GetCommentsByPostId(id);

           return Ok(comments);
        }

        [HttpGet]
        [Route("getByUserId/{id}")]
        public async Task<ActionResult<IEnumerable<CommentShowDto>>> GetCommentsByUserId(string id)
        {
            var comments = await _commentService.GetCommentsByUserId(id);

            return Ok(comments);
        }

    }
}
