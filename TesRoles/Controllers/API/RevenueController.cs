using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesRoles.Data;
using TesRoles.Models;

namespace TesRoles.Controllers.API
{
    [Produces("application/json")]
    [Route("api/Revenue")]
    public class RevenueController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RevenueController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Revenue
        [HttpGet]
        public IEnumerable<Revenue> GetRevenue()
        {
            return _context.Revenue;
        }

        // GET: api/Revenue/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRevenue([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var revenue = await _context.Revenue.Where(m => m.produk.Contains(id)).ToListAsync();

            if (revenue == null)
            {
                return NotFound();
            }

            return Ok(revenue);
        }

        // PUT: api/Revenue/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRevenue([FromRoute] int id, [FromBody] Revenue revenue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != revenue.RevenueId)
            {
                return BadRequest();
            }

            _context.Entry(revenue).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RevenueExists(id))
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

        // POST: api/Revenue
        [HttpPost]
        public async Task<IActionResult> PostRevenue([FromBody] Revenue revenue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Revenue.Add(revenue);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRevenue", new { id = revenue.RevenueId }, revenue);
        }

        // DELETE: api/Revenue/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRevenue([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var revenue = await _context.Revenue.SingleOrDefaultAsync(m => m.RevenueId == id);
            if (revenue == null)
            {
                return NotFound();
            }

            _context.Revenue.Remove(revenue);
            await _context.SaveChangesAsync();

            return Ok(revenue);
        }

        private bool RevenueExists(int id)
        {
            return _context.Revenue.Any(e => e.RevenueId == id);
        }
    }
}