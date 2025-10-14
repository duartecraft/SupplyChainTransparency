using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupplyChainTransparency.Data;
using SupplyChainTransparency.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SupplyChainTransparency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Adiciona autorização a todos os endpoints deste controller
    public class CarbonFootprintsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CarbonFootprintsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CarbonFootprints
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarbonFootprint>>> GetCarbonFootprints(
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            var carbonFootprints = await _context.CarbonFootprints
                .Include(cf => cf.Supplier)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(carbonFootprints);
        }

        // GET: api/CarbonFootprints/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarbonFootprint>> GetCarbonFootprint(int id)
        {
            var carbonFootprint = await _context.CarbonFootprints.Include(cf => cf.Supplier).FirstOrDefaultAsync(cf => cf.Id == id);

            if (carbonFootprint == null)
            {
                return NotFound();
            }

            return carbonFootprint;
        }

        // POST: api/CarbonFootprints
        [HttpPost]
        public async Task<ActionResult<CarbonFootprint>> PostCarbonFootprint(CarbonFootprint carbonFootprint)
        {
            _context.CarbonFootprints.Add(carbonFootprint);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarbonFootprint", new { id = carbonFootprint.Id }, carbonFootprint);
        }

        // PUT: api/CarbonFootprints/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarbonFootprint(int id, CarbonFootprint carbonFootprint)
        {
            if (id != carbonFootprint.Id)
            {
                return BadRequest();
            }

            _context.Entry(carbonFootprint).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarbonFootprintExists(id))
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

        // DELETE: api/CarbonFootprints/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarbonFootprint(int id)
        {
            var carbonFootprint = await _context.CarbonFootprints.FindAsync(id);
            if (carbonFootprint == null)
            {
                return NotFound();
            }

            _context.CarbonFootprints.Remove(carbonFootprint);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarbonFootprintExists(int id)
        {
            return _context.CarbonFootprints.Any(e => e.Id == id);
        }

        // GET: api/CarbonFootprints/bySupplier/5
        [HttpGet("bySupplier/{supplierId}")]
        public async Task<ActionResult<IEnumerable<CarbonFootprint>>> GetCarbonFootprintsBySupplier(int supplierId)
        {
            var carbonFootprints = await _context.CarbonFootprints
                .Where(cf => cf.SupplierId == supplierId)
                .Include(cf => cf.Supplier)
                .ToListAsync();

            if (carbonFootprints == null || !carbonFootprints.Any())
            {
                return NotFound();
            }

            return Ok(carbonFootprints);
        }
    }
}


