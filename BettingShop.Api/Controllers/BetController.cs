using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BettingShop.DataLayer.DB;
using BettingShop.DataLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace BettingShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BetController : ControllerBase
    {
        private readonly IBetRepository repo;
        private readonly IMapper mapper;

        public BetController(IBetRepository repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        // GET: api/Bets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client.Models.Bet>>> GetAll()
        {
            return new(mapper.Map<IEnumerable<Client.Models.Bet>>((await repo.GetAllAsync()).ToList()));
        }

        // GET: api/Bets/5
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Client.Models.Bet>> GetBet(Guid id)
        {
            var betItem = await repo.GetByIdAsync(id);
            if (betItem == null) return NotFound();
            return mapper.Map<Client.Models.Bet>(betItem);
        }

        // PUT: api/Bets/5
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutBet(Guid id, Client.Models.Bet betForUpdating)
        {
            var betModelForUpdating = mapper.Map<BettingShop.DataLayer.Models.Bet>(betForUpdating);
            if (id != betModelForUpdating.Id) return BadRequest();
            var updatedEvent = await repo.UpdateAsync(betModelForUpdating);
            return NoContent();
        }

        // POST: api/Bets
        [HttpPost]
        public async Task<ActionResult<Client.Models.Bet>> PostBet(Client.Models.BetMeta betForCreating)
        {
            var newBetMeta = mapper.Map<BetMeta>(betForCreating);
            var newBet = mapper.Map<BettingShop.DataLayer.Models.Bet>(newBetMeta);
            var createdbet = await repo.CreateAsync(newBet);
            return CreatedAtAction(nameof(GetBet), new { id = newBet.Id }, mapper.Map<Client.Models.Bet>(newBet));
        }

        // DELETE: api/Bets/5
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Client.Models.Bet>> DeleteBet(Guid id)
        {
            var bet = await repo.DeleteAsync(id);
            if (bet == null) return NotFound(); ;
            return mapper.Map<Client.Models.Bet>(bet);
        }
    }
}