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
    [Route("api/Report")]
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Report
        [HttpGet]
        public IEnumerable<YearlyRevenue> GetYearlyRevenue()
        {
            return _context.YearlyRevenue;
        }

        // GET: api/Report/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetYearlyRevenue([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var yearlyRevenue = await _context.YearlyRevenue.SingleOrDefaultAsync(m => m.YearlyRevenueId == id);

            if (yearlyRevenue == null)
            {
                return NotFound();
            }

            return Ok(yearlyRevenue);
        }

        // PUT: api/Report/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutYearlyRevenue([FromRoute] int id, [FromBody] YearlyRevenue yearlyRevenue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != yearlyRevenue.YearlyRevenueId)
            {
                return BadRequest();
            }

            _context.Entry(yearlyRevenue).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!YearlyRevenueExists(id))
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

        // POST: api/Report
        [HttpPost]
        public async Task<IActionResult> PostYearlyRevenue([FromBody] YearlyRevenue yearlyRevenue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.YearlyRevenue.Add(yearlyRevenue);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetYearlyRevenue", new { id = yearlyRevenue.YearlyRevenueId }, yearlyRevenue);
        }

        // DELETE: api/Report/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteYearlyRevenue([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var yearlyRevenue = await _context.YearlyRevenue.SingleOrDefaultAsync(m => m.YearlyRevenueId == id);
            if (yearlyRevenue == null)
            {
                return NotFound();
            }

            _context.YearlyRevenue.Remove(yearlyRevenue);
            await _context.SaveChangesAsync();

            return Ok(yearlyRevenue);
        }

        private bool YearlyRevenueExists(int id)
        {
            return _context.YearlyRevenue.Any(e => e.YearlyRevenueId == id);
        }
    }
}