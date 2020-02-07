using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Askianoor.Models;
using Microsoft.AspNetCore.Authorization;

namespace Askianoor.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class PortfolioCategoriesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public PortfolioCategoriesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/PortfolioCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PortfolioCategory>>> GetPortfolioCategories()
        {
            return await _context.PortfolioCategories.ToListAsync();
        }

        // GET: api/PortfolioCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PortfolioCategory>> GetPortfolioCategory(Guid id)
        {
            var portfolioCategory = await _context.PortfolioCategories.FindAsync(id);

            if (portfolioCategory == null)
            {
                return NotFound();
            }

            return portfolioCategory;
        }

        // PUT: api/PortfolioCategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPortfolioCategory(Guid id, PortfolioCategory portfolioCategory)
        {
            if (id != portfolioCategory.PortfolioCategoryId)
            {
                return BadRequest();
            }

            _context.Entry(portfolioCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PortfolioCategoryExists(id))
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

        // POST: api/PortfolioCategories
        [HttpPost]
        public async Task<ActionResult<PortfolioCategory>> PostPortfolioCategory(PortfolioCategory portfolioCategory)
        {
            _context.PortfolioCategories.Add(portfolioCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPortfolioCategory", new { id = portfolioCategory.PortfolioCategoryId }, portfolioCategory);
        }

        // DELETE: api/PortfolioCategories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PortfolioCategory>> DeletePortfolioCategory(Guid id)
        {
            var portfolioCategory = await _context.PortfolioCategories.FindAsync(id);
            if (portfolioCategory == null)
            {
                return NotFound();
            }

            _context.PortfolioCategories.Remove(portfolioCategory);
            await _context.SaveChangesAsync();

            return portfolioCategory;
        }

        private bool PortfolioCategoryExists(Guid id)
        {
            return _context.PortfolioCategories.Any(e => e.PortfolioCategoryId == id);
        }
    }
}
