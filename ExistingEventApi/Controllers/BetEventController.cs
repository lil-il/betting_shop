using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BettingShop.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BettingShop.Api.Controllers
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

        // GET: api/ExistingEvents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client.Models.BetEvent>>> GetAllAsync()
        {
            return new(mapper.Map<IEnumerable<Client.Models.BetEvent>>(await context.ExistingEvents.ToListAsync()));
        }

        // GET: api/ExistingEvents/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Client.Models.BetEvent>> GetOneEvent(int id)
        {
            var eventItem = await context.ExistingEvents.FindAsync(id);
            if (eventItem == null) return NotFound();
            return mapper.Map<Client.Models.BetEvent>(eventItem);
        }

        // PUT: api/ExistingEvents/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutEventAsync(int id, Client.Models.BetEvent eventForUpdating)
        {
            var betEventForUpdating = mapper.Map<BetEvent>(eventForUpdating);
            if (id != betEventForUpdating.Id) return BadRequest();
            context.Entry(betEventForUpdating).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/ExistingEvents
        [HttpPost]
        public async Task<ActionResult<Client.Models.BetEvent>> PostEventAsync(Client.Models.BetEventMeta eventForCreating)
        {
            var newEventMeta = mapper.Map<BetEventMeta>(eventForCreating);
            var newEvent = mapper.Map<BetEvent>(newEventMeta);
            context.ExistingEvents.Add(newEvent);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOneEvent), new {id = newEvent.Id}, mapper.Map<Client.Models.BetEvent>(newEvent));
        }

        // DELETE: api/ExistingEvents/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Client.Models.BetEvent>> DeleteEventAsync(int id)
        {
            var existingEvent = await context.ExistingEvents.FindAsync(id);
            if (existingEvent == null) return NotFound();
            context.ExistingEvents.Remove(existingEvent);
            await context.SaveChangesAsync();
            return mapper.Map<Client.Models.BetEvent>(existingEvent);
        }
    }
}