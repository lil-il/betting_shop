using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BettingShop.DataLayer.Models;
using BettingShop.DataLayer.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BettingShop.Api.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository repo;
        private readonly IMapper mapper;

        public UserController(IUserRepository repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client.Models.User>>> GetAll()
        {
            return new(mapper.Map<IEnumerable<Client.Models.User>>(await repo.GetAllAsync()).ToList());
        }

        // GET: api/Users/5
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Client.Models.User>> GetUser(Guid id)
        {
            var user = await repo.GetByIdAsync(id);
            if (user == null) return NotFound();
            return mapper.Map<Client.Models.User>(user);
        }

        // PUT: api/Users/5
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutUser(Guid id, Client.Models.User userForUpdating)
        {
            var userModelForUpdating = mapper.Map<User>(userForUpdating);
            if (id != userModelForUpdating.Id) return BadRequest();
            var updatedBet = await repo.UpdateAsync(userModelForUpdating);
            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<Client.Models.User>> PostUser(Client.Models.UserMeta userForCreating)
        {
            var newUserMeta = mapper.Map<UserMeta>(userForCreating);
            var newUser = mapper.Map<User>(newUserMeta);
            var createdUser = await repo.CreateAsync(newUser);
            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, mapper.Map<Client.Models.User>(newUser));
        }

        // DELETE: api/Users/5
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Client.Models.User>> DeleteUser(Guid id)
        {
            var user = await repo.DeleteAsync(id);
            if (user == null) return NotFound();
            return mapper.Map<Client.Models.User>(user);
        }
    }
}