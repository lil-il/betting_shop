using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BettingShop.DataLayer.DB;
using BettingShop.DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BetEvent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BetEventController : ControllerBase
    {
        private readonly IEventRepository repo;
        private readonly IMapper mapper;

        public BetEventController(IEventRepository repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        // GET: api/ExistingEvents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client.Models.BetEvent>>> GetAll()
        {
            return new(mapper.Map<IEnumerable<Client.Models.BetEvent>>((await repo.GetExistingEventsAsync()).ToList()));
        }

        // GET: api/ExistingEvents/5
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Client.Models.BetEvent>> GetEvent(Guid id)
        {
            var eventItem = await repo.GetExistingEventByIdAsync(id);
            if (eventItem == null) return NotFound();
            return mapper.Map<Client.Models.BetEvent>(eventItem);
        }

        // PUT: api/ExistingEvents/5
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutEvent(Guid id, Client.Models.BetEvent eventForUpdating)
        {
            var betEventForUpdating = mapper.Map<BettingShop.DataLayer.Models.BetEvent>(eventForUpdating);
            if (id != betEventForUpdating.Id) return BadRequest();
            var updatedEvent = await repo.UpdateAsync(betEventForUpdating);
            return NoContent();
        }

        // POST: api/ExistingEvents
        [HttpPost]
        public async Task<ActionResult<Client.Models.BetEvent>> PostEvent(Client.Models.BetEventMeta eventForCreating)
        {
            var newEventMeta = mapper.Map<BetEventMeta>(eventForCreating);
            var newEvent = mapper.Map<BettingShop.DataLayer.Models.BetEvent>(newEventMeta);
            var createdEvent = await repo.CreateAsync(newEvent);
            return CreatedAtAction(nameof(GetEvent), new { id = newEvent.Id }, mapper.Map<Client.Models.BetEvent>(newEvent));
        }

        // DELETE: api/ExistingEvents/5
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Client.Models.BetEvent>> DeleteEvent(Guid id)
        {
            var existingEvent = await repo.DeleteAsync(id);
            if (existingEvent == null) return NotFound();
            return mapper.Map<Client.Models.BetEvent>(existingEvent);
        }
    }
}