using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ExistingEventApi.DTOs;
using ExistingEventApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExistingEventApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExistingEventsController : ControllerBase
    {
        private readonly ExistingEventContext context;
        private readonly IMapper mapper;

        public ExistingEventsController(ExistingEventContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/ExistingEvents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventReadDTO>>> GetAll()
        {
            return new (mapper.Map<IEnumerable<EventReadDTO>>(await context.ExistingEvents.ToListAsync()));
        }

        // GET: api/ExistingEvents/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<EventReadDTO>> GetEvent(int id)
        {
            var eventItem = await context.ExistingEvents.FindAsync(id);
            if (eventItem == null)
                return NotFound();
            return mapper.Map<EventReadDTO>(eventItem);
        }

        // PUT: api/ExistingEvents/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutEvent(int id, EventUpdateDTO eventUpdateDto)
        {
            var eventReadDTOForUpdating = GetEvent(id);
            if (eventUpdateDto == null || eventReadDTOForUpdating == null)
                return BadRequest();
            var eventForUpdating = mapper.Map<ExistingEvent>(eventReadDTOForUpdating);
            context.Entry(eventForUpdating).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/ExistingEvents
        [HttpPost]
        public async Task<ActionResult<EventReadDTO>> PostEvent(EventCreateDTO eventCreateDTO)
        {
            var newEvent = mapper.Map<ExistingEvent>(eventCreateDTO);
            context.ExistingEvents.Add(newEvent);
            await context.SaveChangesAsync();
            var eventReadDTO = mapper.Map<EventReadDTO>(newEvent);
            return CreatedAtAction(nameof(GetEvent), new { id = newEvent.Id },
                eventReadDTO);
        }

        // DELETE: api/ExistingEvents/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var existingEvent = await context.ExistingEvents.FindAsync(id);
            if (existingEvent == null)
                return NotFound();
            context.ExistingEvents.Remove(existingEvent);
            await context.SaveChangesAsync();
            return NoContent();
        }

        private bool EventExists(int id)
        {
            return context.ExistingEvents.Any(e => e.Id.CompareTo(id) == 0);
        }
    }
}