﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SavingsProjection.API.Infrastructure;
using SavingsProjection.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SavingsProjection.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RecurrencyAdjustementsController : ControllerBase
    {
        private readonly SavingProjectionContext _context;

        public RecurrencyAdjustementsController(SavingProjectionContext context)
        {
            _context = context;
        }

        // GET: api/RecurrencyAdjustements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecurrencyAdjustement>>> GetRecurrencyAdjustements()
        {
            return await _context.RecurrencyAdjustements.ToListAsync();
        }


        // GET: api/RecurrencyAdjustements/ByIDRecurrency/5
        [HttpGet("ByIDRecurrency/{idRecurrency}", Name = "ByIDRecurrency")]
        public async Task<ActionResult<RecurrencyAdjustement>> GetByIDRecurrency(long idRecurrency)
        {
            return await _context.RecurrencyAdjustements.Where(x => x.RecurrentMoneyItemID == idRecurrency).FirstOrDefaultAsync();
        }

        // GET: api/RecurrencyAdjustements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecurrencyAdjustement>> GetRecurrencyAdjustement(long id)
        {
            var recurrencyAdjustement = await _context.RecurrencyAdjustements.FindAsync(id);

            if (recurrencyAdjustement == null)
            {
                return NotFound();
            }

            return recurrencyAdjustement;
        }

        // PUT: api/RecurrencyAdjustements/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecurrencyAdjustement(long id, RecurrencyAdjustement recurrencyAdjustement)
        {
            if (id != recurrencyAdjustement.ID)
            {
                return BadRequest();
            }

            _context.Entry(recurrencyAdjustement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecurrencyAdjustementExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/RecurrencyAdjustements
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<RecurrencyAdjustement>> PostRecurrencyAdjustement(RecurrencyAdjustement recurrencyAdjustement)
        {
            _context.RecurrencyAdjustements.Add(recurrencyAdjustement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecurrencyAdjustement", new { id = recurrencyAdjustement.ID }, recurrencyAdjustement);
        }

        // DELETE: api/RecurrencyAdjustements/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RecurrencyAdjustement>> DeleteRecurrencyAdjustement(long id)
        {
            var recurrencyAdjustement = await _context.RecurrencyAdjustements.FindAsync(id);
            if (recurrencyAdjustement == null)
            {
                return NotFound();
            }

            _context.RecurrencyAdjustements.Remove(recurrencyAdjustement);
            await _context.SaveChangesAsync();

            return recurrencyAdjustement;
        }

        private bool RecurrencyAdjustementExists(long id)
        {
            return _context.RecurrencyAdjustements.Any(e => e.ID == id);
        }
    }
}
