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
    public class EducationsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public EducationsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Educations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Education>>> GetEducations()
        {
            return await _context.Educations.ToListAsync().ConfigureAwait(true);
        }

        // GET: api/Educations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Education>> GetEducation(Guid id)
        {
            var education = await _context.Educations.FindAsync(id);

            if (education == null)
            {
                return NotFound();
            }

            return education;
        }

        // PUT: api/Educations/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutEducation(Guid id, Education education)
        {
            if (education == null || id != education.EducationId)
            {
                return BadRequest();
            }

            _context.Entry(education).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(true);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EducationExists(id))
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

        // POST: api/Educations
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Education>> PostEducation(Education education)
        {
            if (education == null)
            {
                return NotFound();
            }

            _context.Educations.Add(education);
            await _context.SaveChangesAsync().ConfigureAwait(true);

            return CreatedAtAction("GetEducation", new { id = education.EducationId }, education);
        }

        // DELETE: api/Educations/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Education>> DeleteEducation(Guid id)
        {
            var education = await _context.Educations.FindAsync(id);
            if (education == null)
            {
                return NotFound();
            }

            _context.Educations.Remove(education);
            await _context.SaveChangesAsync().ConfigureAwait(true); ;

            return education;
        }

        private bool EducationExists(Guid id)
        {
            return _context.Educations.Any(e => e.EducationId == id);
        }
    }
}
