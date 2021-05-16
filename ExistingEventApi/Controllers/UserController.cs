using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserApi.Models;

namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext context;

        public UserController(UserContext context)
        {
            this.context = context;
        }

        // GET: api/ExistingEvents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            return await context.Users.ToListAsync();
        }

        // GET: api/ExistingEvents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var todoItem = await context.Users.FindAsync(id);
            if (todoItem == null)
                return NotFound();
            return todoItem;
        }

        // PUT: api/ExistingEvents/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, User modifiedUser)
        {
            if (modifiedUser == null || !modifiedUser.Id.Equals(id))
                return BadRequest();
            context.Entry(modifiedUser).State = EntityState.Modified;
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                    return NotFound();
                throw;
            }
            return NoContent();
        }

        // POST: api/ExistingEvents
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User newUser)
        {
            context.Users.Add(newUser);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id },
                newUser);
        }

        // DELETE: api/ExistingEvents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var existingUser = await context.Users.FindAsync(id);
            if (existingUser == null)
                return NotFound();
            context.Users.Remove(existingUser);
            await context.SaveChangesAsync();
            return NoContent();
        }

        private bool UserExists(Guid id)
        {
            return context.Users.Any(e => e.Id.CompareTo(id) == 0);
        }
    }
}