using Assesment.Data;
using Assesment.DTO;
using Assesment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assesment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserProfileController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserProfileDTO>>> GetAllUsers()
        {
            var users = await _context.UserProfiles.ToListAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserProfileDTO>> GetUserById(int id)
        {
            var user = await _context.UserProfiles.FirstOrDefaultAsync(u => u.UserID == id);
            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserProfileDTO>> CreateUser(UserProfileDTO dto)
        {
            var user = new UserProfile { /* Map dto properties */ };
            _context.UserProfiles.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserById), new { id = user.UserID }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserProfileDTO dto)
        {
            if (id != dto.UserID) return BadRequest();
            var user = await _context.UserProfiles.FindAsync(id);
            if (user == null) return NotFound();

            // Update fields
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.UserProfiles.FindAsync(id);
            if (user == null) return NotFound();

            _context.UserProfiles.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

