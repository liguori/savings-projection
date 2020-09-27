﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SavingsProjection.API.Infrastructure;
using SavingsProjection.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SavingsProjection.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterializedMoneyItemsController : ControllerBase
    {
        private readonly SavingProjectionContext _context;

        public MaterializedMoneyItemsController(SavingProjectionContext context)
        {
            _context = context;
        }

        // GET: api/MaterializedMoneyItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterializedMoneyItem>>> GetMaterializedMoneyItems()
        {
            return await _context.MaterializedMoneyItems.ToListAsync();
        }

        // GET: api/MaterializedMoneyItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MaterializedMoneyItem>> GetMaterializedMoneyItem(long id)
        {
            var materializedMoneyItem = await _context.MaterializedMoneyItems.FindAsync(id);

            if (materializedMoneyItem == null)
            {
                return NotFound();
            }

            return materializedMoneyItem;
        }

        // PUT: api/MaterializedMoneyItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaterializedMoneyItem(long id, MaterializedMoneyItem materializedMoneyItem)
        {
            if (id != materializedMoneyItem.ID)
            {
                return BadRequest();
            }

            _context.Entry(materializedMoneyItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterializedMoneyItemExists(id))
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

        // POST: api/MaterializedMoneyItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MaterializedMoneyItem>> PostMaterializedMoneyItem(MaterializedMoneyItem materializedMoneyItem)
        {
            _context.MaterializedMoneyItems.Add(materializedMoneyItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMaterializedMoneyItem", new { id = materializedMoneyItem.ID }, materializedMoneyItem);
        }

        // DELETE: api/MaterializedMoneyItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MaterializedMoneyItem>> DeleteMaterializedMoneyItem(long id)
        {
            var materializedMoneyItem = await _context.MaterializedMoneyItems.FindAsync(id);
            if (materializedMoneyItem == null)
            {
                return NotFound();
            }

            _context.MaterializedMoneyItems.Remove(materializedMoneyItem);
            await _context.SaveChangesAsync();

            return materializedMoneyItem;
        }

        private bool MaterializedMoneyItemExists(long id)
        {
            return _context.MaterializedMoneyItems.Any(e => e.ID == id);
        }
    }
}
