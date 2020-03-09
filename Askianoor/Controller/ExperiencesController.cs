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
    public class ExperiencesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public ExperiencesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Experiences
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Experience>>> GetExperiences()
        {
            return await _context.Experiences.ToListAsync().ConfigureAwait(true);
        }

        // GET: api/Experiences/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Experience>> GetExperience(Guid id)
        {
            var experience = await _context.Experiences.FindAsync(id);

            if (experience == null)
            {
                return NotFound();
            }

            return experience;
        }

        // PUT: api/Experiences/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExperience(Guid id, Experience experience)
        {
            if (experience == null || id != experience.ExperienceId)
            {
                return BadRequest();
            }

            _context.Entry(experience).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(true);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExperienceExists(id))
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

        // POST: api/Experiences
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Experience>> PostExperience(Experience experience)
        {
            if (experience == null)
            {
                return NotFound();
            }

            _context.Experiences.Add(experience);
            await _context.SaveChangesAsync().ConfigureAwait(true);

            return CreatedAtAction("GetExperience", new { id = experience.ExperienceId }, experience);
        }

        // DELETE: api/Experiences/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Experience>> DeleteExperience(Guid id)
        {
            var experience = await _context.Experiences.FindAsync(id);
            if (experience == null)
            {
                return NotFound();
            }

            _context.Experiences.Remove(experience);
            await _context.SaveChangesAsync().ConfigureAwait(true);

            return experience;
        }

        private bool ExperienceExists(Guid id)
        {
            return _context.Experiences.Any(e => e.ExperienceId == id);
        }
    }
}
