using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Askianoor.Models;

namespace Askianoor.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class NavbarsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public NavbarsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Navbars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Navbar>>> GetNavbars()
        {
            return await _context.Navbars.ToListAsync().ConfigureAwait(true);
        }

        // GET: api/Navbars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Navbar>> GetNavbar(Guid id)
        {
            var navbar = await _context.Navbars.FindAsync(id);

            if (navbar == null)
            {
                return NotFound();
            }

            return navbar;
        }

        // PUT: api/Navbars/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNavbar(Guid id, Navbar navbar)
        {
            if (navbar == null || id != navbar.MenuId)
            {
                return BadRequest();
            }

            _context.Entry(navbar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(true);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NavbarExists(id))
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

        // POST: api/Navbars
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Navbar>> PostNavbar(Navbar navbar)
        {
            if (navbar == null)
                return BadRequest();

            _context.Navbars.Add(navbar);
            await _context.SaveChangesAsync().ConfigureAwait(true);

            return CreatedAtAction("GetNavbar", new { id = navbar.MenuId }, navbar);
        }

        // DELETE: api/Navbars/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Navbar>> DeleteNavbar(Guid id)
        {
            var navbar = await _context.Navbars.FindAsync(id);
            if (navbar == null)
            {
                return NotFound();
            }

            _context.Navbars.Remove(navbar);
            await _context.SaveChangesAsync().ConfigureAwait(true);

            return navbar;
        }

        private bool NavbarExists(Guid id)
        {
            return _context.Navbars.Any(e => e.MenuId == id);
        }
    }
}
