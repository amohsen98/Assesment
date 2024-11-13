using Assesment.Data;
using Assesment.DTO;
using Assesment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Assesment.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserProfileController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UserProfile
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserProfile>>> GetUserProfiles()
        {
            var userProfiles = await _context.UserProfiles.ToListAsync();
            return Ok(userProfiles); // Return the list of user profiles
        }

        // GET: api/UserProfile/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserProfile>> GetUserProfile(int id)
        {
            // Validate the ID
            if (id <= 0)
            {
                return BadRequest("Invalid ID.");
            }

            var userProfile = await _context.UserProfiles.FindAsync(id);
            if (userProfile == null)
            {
                return NotFound();
            }

            return Ok(userProfile);
        }

        // POST: api/UserProfile
        [HttpPost]
        public async Task<ActionResult<UserProfile>> CreateUserProfile([FromBody] UserProfile userProfile)
        {
            // Validate model state
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return validation errors
            }

            // Sanitize input fields to avoid XSS (for text fields like FirstName, LastName, Email)
            userProfile.FirstName = WebUtility.HtmlEncode(userProfile.FirstName);
            userProfile.LastName = WebUtility.HtmlEncode(userProfile.LastName);
            userProfile.Email = WebUtility.HtmlEncode(userProfile.Email); // Sanitize Email as well

            // Add user profile to the context
            _context.UserProfiles.Add(userProfile);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            return CreatedAtAction(nameof(GetUserProfile), new { id = userProfile.UserID }, userProfile);
        }

        // PUT: api/UserProfile/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserProfile(int id, [FromBody] UserProfile userProfile)
        {
            // Validate the ID and model state
            if (id <= 0 || id != userProfile.UserID || !ModelState.IsValid)
            {
                return BadRequest("Invalid data provided.");
            }

            // Sanitize input fields
            userProfile.FirstName = WebUtility.HtmlEncode(userProfile.FirstName);
            userProfile.LastName = WebUtility.HtmlEncode(userProfile.LastName);
            userProfile.Email = WebUtility.HtmlEncode(userProfile.Email);

            // Check if the user profile exists
            var existingProfile = await _context.UserProfiles.FindAsync(id);
            if (existingProfile == null)
            {
                return NotFound();
            }

            _context.Entry(existingProfile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserProfileExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // No content means success but nothing to return
        }

        // DELETE: api/UserProfile/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserProfile(int id)
        {
            // Validate the ID
            if (id <= 0)
            {
                return BadRequest("Invalid ID.");
            }

            // Retrieve the user profile from the database
            var userProfile = await _context.UserProfiles.FindAsync(id);
            if (userProfile == null)
            {
                return NotFound();
            }

            // Remove the profile from the context and save changes
            _context.UserProfiles.Remove(userProfile);
            await _context.SaveChangesAsync();

            return NoContent(); // No content means the delete operation was successful
        }

        private bool UserProfileExists(int id)
        {
            return _context.UserProfiles.Any(e => e.UserID == id);
        }
    }

}

