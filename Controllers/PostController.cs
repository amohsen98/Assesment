using Assesment.Data;
using Assesment.DTO;
using Assesment.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Assesment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PostController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Post
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetPosts()
        {
            var posts = await _context.Posts.ToListAsync();
            return Ok(_mapper.Map<List<PostDTO>>(posts));
        }

        // GET: api/Post/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PostDTO>> GetPost(int id)
        {
            // Validate the id parameter to prevent invalid data types (e.g., SQL injection)
            if (id <= 0)
            {
                return BadRequest("Invalid ID.");
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PostDTO>(post));
        }

        // POST: api/Post
        [HttpPost]
        public async Task<ActionResult<PostDTO>> CreatePost([FromBody] PostCreateDTO postCreateDTO)
        {
            // Validate the incoming model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return validation errors if any
            }

            // Sanitize input to avoid XSS (especially for HTML-sensitive fields like Content)

            postCreateDTO.Content = WebUtility.HtmlEncode(postCreateDTO.Content); // Prevent XSS
            postCreateDTO.Title = WebUtility.HtmlEncode(postCreateDTO.Title); // Prevent XSS

            var post = _mapper.Map<Post>(postCreateDTO);
            post.DatePosted = DateTime.UtcNow;

            // Add to database
            _context.Posts.Add(post);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Handle database errors (e.g., unique constraint violations)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            return CreatedAtAction(nameof(GetPost), new { id = post.PostID }, _mapper.Map<PostDTO>(post));
        }

        // PUT: api/Post/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] PostUpdateDTO postUpdateDTO)
        {
            if (id <= 0 || !ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return validation errors if any
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            // Sanitize input to avoid XSS (especially for HTML-sensitive fields like Content)
            postUpdateDTO.Content = WebUtility.HtmlEncode(postUpdateDTO.Content); // Prevent XSS

            // Map updated data from DTO to entity
            _mapper.Map(postUpdateDTO, post);

            try
            {
                _context.Entry(post).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // Success, no content to return
        }

        // DELETE: api/Post/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID.");
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            // Remove the post from the database
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent(); // Success, no content to return
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.PostID == id);
        }
    }
}
