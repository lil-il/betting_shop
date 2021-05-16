using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ExistingEventApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExistingEventApi.Controllers
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
        public async Task<ActionResult<IEnumerable<ApiClient.Models.BetEvent>>> GetAll()
        {
            return new(mapper.Map<IEnumerable<ApiClient.Models.BetEvent>>(await context.ExistingEvents.ToListAsync()));
        }

        // GET: api/ExistingEvents/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiClient.Models.BetEvent>> GetEvent(int id)
        {
            var eventItem = await context.ExistingEvents.FindAsync(id);
            if (eventItem == null) return NotFound();
            return mapper.Map<ApiClient.Models.BetEvent>(eventItem);
        }

        // PUT: api/ExistingEvents/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutEvent(int id, ApiClient.Models.BetEvent eventForUpdating)
        {
            var betEventForUpdating = mapper.Map<BetEvent>(eventForUpdating);
            if (id != betEventForUpdating.Id) return BadRequest();
            context.Entry(betEventForUpdating).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/ExistingEvents
        [HttpPost]
        public async Task<ActionResult<ApiClient.Models.BetEvent>> PostEvent(ApiClient.Models.BetEventMeta eventForCreating)
        {
            var newEventMeta = mapper.Map<BetEventMeta>(eventForCreating);
            var newEvent = mapper.Map<BetEvent>(newEventMeta);
            context.ExistingEvents.Add(newEvent);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEvent), new {id = newEvent.Id}, mapper.Map<ApiClient.Models.BetEvent>(newEvent));
        }

        // DELETE: api/ExistingEvents/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var existingEvent = await context.ExistingEvents.FindAsync(id);
            if (existingEvent == null) return NotFound();
            context.ExistingEvents.Remove(existingEvent);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}