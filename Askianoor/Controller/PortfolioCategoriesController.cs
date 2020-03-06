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
            return await _context.PortfolioCategories.ToListAsync().ConfigureAwait(true);
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutPortfolioCategory(Guid id, PortfolioCategory portfolioCategory)
        {

            if (portfolioCategory == null || id != portfolioCategory.PortfolioCategoryId)
            {
                return BadRequest();
            }

            _context.Entry(portfolioCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(true);
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<PortfolioCategory>> PostPortfolioCategory(PortfolioCategory portfolioCategory)
        {
            if (portfolioCategory == null)
            {
                return BadRequest();
            }

            _context.PortfolioCategories.Add(portfolioCategory);
            await _context.SaveChangesAsync().ConfigureAwait(true);

            return CreatedAtAction("GetPortfolioCategory", new { id = portfolioCategory.PortfolioCategoryId }, portfolioCategory);
        }

        // DELETE: api/PortfolioCategories/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<PortfolioCategory>> DeletePortfolioCategory(Guid id)
        {
            var portfolioCategory = await _context.PortfolioCategories.FindAsync(id);
            if (portfolioCategory == null)
            {
                return NotFound();
            }

            _context.PortfolioCategories.Remove(portfolioCategory);
            await _context.SaveChangesAsync().ConfigureAwait(true);

            return portfolioCategory;
        }

        private bool PortfolioCategoryExists(Guid id)
        {
            return _context.PortfolioCategories.Any(e => e.PortfolioCategoryId == id);
        }
    }
}
