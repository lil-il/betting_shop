using System.Collections.Generic;
using System.Threading.Tasks;
using ApiClient.Models;
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
        private readonly ExistingEventContext context;
        private readonly IMapper mapper;

        public BetEventController(ExistingEventContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/ExistingEvents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BetEvent>>> GetAll()
        {
            return new(mapper.Map<IEnumerable<BetEvent>>(await context.ExistingEvents.ToListAsync()));
        }

        // GET: api/ExistingEvents/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<BetEvent>> GetEvent(int id)
        {
            var eventItem = await context.ExistingEvents.FindAsync(id);
            if (eventItem == null) return NotFound();
            return mapper.Map<BetEvent>(eventItem);
        }

        // PUT: api/ExistingEvents/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutEvent(BetEvent eventUpdateDto)
        {
            var eventReadDTOForUpdating = GetEvent(eventUpdateDto.Id);
            if (eventUpdateDto == null || eventReadDTOForUpdating == null) return BadRequest();
            var eventForUpdating = mapper.Map<ExistingEvent>(eventReadDTOForUpdating);
            context.Entry(eventForUpdating).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/ExistingEvents
        [HttpPost]
        public async Task<ActionResult<BetEvent>> PostEvent(BetEventMeta eventCreateDTO)
        {
            var newEvent = mapper.Map<ExistingEvent>(eventCreateDTO);
            context.ExistingEvents.Add(newEvent);
            await context.SaveChangesAsync();
            var eventReadDTO = mapper.Map<BetEvent>(newEvent);
            return CreatedAtAction(nameof(GetEvent), new {id = newEvent.Id}, eventReadDTO);
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