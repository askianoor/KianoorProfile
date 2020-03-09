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
    public class SocialNetworksController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public SocialNetworksController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/SocialNetworks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SocialNetwork>>> GetSocialNetworks()
        {
            return await _context.SocialNetworks.ToListAsync().ConfigureAwait(true);
        }

        // GET: api/SocialNetworks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SocialNetwork>> GetSocialNetwork(Guid id)
        {
            var socialNetwork = await _context.SocialNetworks.FindAsync(id);

            if (socialNetwork == null)
            {
                return NotFound();
            }

            return socialNetwork;
        }

        // PUT: api/SocialNetworks/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSocialNetwork(Guid id, SocialNetwork socialNetwork)
        {
            if (socialNetwork == null || id != socialNetwork.SocialId)
            {
                return BadRequest();
            }

            _context.Entry(socialNetwork).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(true);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SocialNetworkExists(id))
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

        // POST: api/SocialNetworks
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<SocialNetwork>> PostSocialNetwork(SocialNetwork socialNetwork)
        {
            if (socialNetwork == null)
            {
                return NotFound();
            }

            _context.SocialNetworks.Add(socialNetwork);
            await _context.SaveChangesAsync().ConfigureAwait(true);

            return CreatedAtAction("GetSocialNetwork", new { id = socialNetwork.SocialId }, socialNetwork);
        }

        // DELETE: api/SocialNetworks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SocialNetwork>> DeleteSocialNetwork(Guid id)
        {
            var socialNetwork = await _context.SocialNetworks.FindAsync(id);
            if (socialNetwork == null)
            {
                return NotFound();
            }

            _context.SocialNetworks.Remove(socialNetwork);
            await _context.SaveChangesAsync().ConfigureAwait(true);

            return socialNetwork;
        }

        private bool SocialNetworkExists(Guid id)
        {
            return _context.SocialNetworks.Any(e => e.SocialId == id);
        }
    }
}
