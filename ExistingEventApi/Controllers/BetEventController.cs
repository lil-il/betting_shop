using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BetEvent.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BetEvent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BetEventController : ControllerBase
    {
        private readonly BetEventContext context;
        private readonly IMapper mapper;

        public BetEventController(BetEventContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/Events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client.Models.BetEvent>>> GetAll()
        {
            return new(mapper.Map<IEnumerable<Client.Models.BetEvent>>(await context.Events.ToListAsync()));
        }

        // GET: api/Events/5
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Client.Models.BetEvent>> GetEvent(Guid id)
        {
            var eventItem = await context.Events.FindAsync(id);
            if (eventItem == null) return NotFound();
            return mapper.Map<Client.Models.BetEvent>(eventItem);
        }

        // PUT: api/Events/5
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutEvent(Guid id, Client.Models.BetEvent eventForUpdating)
        {
            var betEventForUpdating = mapper.Map<Models.BetEvent>(eventForUpdating);
            if (id != betEventForUpdating.Id) return BadRequest();
            context.Entry(betEventForUpdating).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Events
        [HttpPost]
        public async Task<ActionResult<Client.Models.BetEvent>> PostEvent(Client.Models.BetEventMeta eventForCreating)
        {
            var newEventMeta = mapper.Map<BetEventMeta>(eventForCreating);
            var newEvent = mapper.Map<Models.BetEvent>(newEventMeta);
            context.Events.Add(newEvent);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEvent), new {id = newEvent.Id}, mapper.Map<Client.Models.BetEvent>(newEvent));
        }

        // DELETE: api/Events/5
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Client.Models.BetEvent>> DeleteEvent(Guid id)
        {
            var existingEvent = await context.Events.FindAsync(id);
            if (existingEvent == null) return NotFound();
            context.Events.Remove(existingEvent);
            await context.SaveChangesAsync();
            return mapper.Map<Client.Models.BetEvent>(existingEvent);
        }
    }
}