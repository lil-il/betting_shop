using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BetEvent.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BetEvent.Api.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext context;
        private readonly IMapper mapper;

        public UserController(UserContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client.Models.User>>> GetAll()
        {
            return new(mapper.Map<IEnumerable<Client.Models.User>>(await context.Users.ToListAsync()));
        }

        // GET: api/Users/5
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Client.Models.User>> GetUser(Guid id)
        {
            var user = await context.Users.FindAsync(id);
            if (user == null) return NotFound();
            return mapper.Map<Client.Models.User>(user);
        }

        // PUT: api/Users/5
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutUser(Guid id, Client.Models.User userForUpdating)
        {
            var userModelForUpdating = mapper.Map<User>(userForUpdating);
            if (id != userModelForUpdating.Id) return BadRequest();
            context.Entry(userModelForUpdating).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<Client.Models.User>> PostUser(Client.Models.UserMeta userForCreating)
        {
            var newUserMeta = mapper.Map<UserMeta>(userForCreating);
            var newUser = mapper.Map<User>(newUserMeta);
            context.Users.Add(newUser);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, mapper.Map<Client.Models.User>(newUser));
        }

        // DELETE: api/Users/5
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Client.Models.User>> DeleteUser(Guid id)
        {
            var user = await context.Users.FindAsync(id);
            if (user == null) return NotFound();
            context.Users.Remove(user);
            await context.SaveChangesAsync();
            return mapper.Map<Client.Models.User>(user);
        }
    }
}