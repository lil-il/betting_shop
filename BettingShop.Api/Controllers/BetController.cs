using System;
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
    public class BetController : ControllerBase
    {
        private readonly BetContext context;
        private readonly IMapper mapper;

        public BetController(BetContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/Bets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client.Models.Bet>>> GetAll()
        {
            return new(mapper.Map<IEnumerable<Client.Models.Bet>>(await context.Bets.ToListAsync()));
        }

        // GET: api/Bets/5
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Client.Models.Bet>> GetBet(Guid id)
        {
            var betItem = await context.Bets.FindAsync(id);
            if (betItem == null) return NotFound();
            return mapper.Map<Client.Models.Bet>(betItem);
        }

        // PUT: api/Bets/5
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutBet(Guid id, Client.Models.Bet betForUpdating)
        {
            var betModelForUpdating = mapper.Map<Bet>(betForUpdating);
            if (id != betModelForUpdating.Id) return BadRequest();
            context.Entry(betModelForUpdating).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Bets
        [HttpPost]
        public async Task<ActionResult<Client.Models.Bet>> PostBet(Client.Models.BetMeta betForCreating)
        {
            var newBetMeta = mapper.Map<BetMeta>(betForCreating);
            var newBet = mapper.Map<Bet>(newBetMeta);
            context.Bets.Add(newBet);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBet), new { id = newBet.Id }, mapper.Map<Client.Models.Bet>(newBet));
        }

        // DELETE: api/Bets/5
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Client.Models.Bet>> DeleteBet(Guid id)
        {
            var bet = await context.Bets.FindAsync(id);
            if (bet == null) return NotFound();
            context.Bets.Remove(bet);
            await context.SaveChangesAsync();
            return mapper.Map<Client.Models.Bet>(bet);
        }
    }
}