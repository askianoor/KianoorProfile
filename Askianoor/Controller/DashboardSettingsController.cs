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
    public class DashboardSettingsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public DashboardSettingsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/DashboardSettings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DashboardSetting>>> GetDashboardSettings()
        {
            return await _context.DashboardSettings.ToListAsync().ConfigureAwait(true);
        }

        // GET: api/DashboardSettings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DashboardSetting>> GetDashboardSetting(int id)
        {
            var dashboardSetting = await _context.DashboardSettings.FindAsync(id);

            if (dashboardSetting == null)
            {
                return NotFound();
            }

            return dashboardSetting;
        }

        // PUT: api/DashboardSettings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDashboardSetting(int id, DashboardSetting dashboardSetting)
        {
            if (dashboardSetting == null || id != dashboardSetting.Id)
            {
                return BadRequest();
            }

            _context.Entry(dashboardSetting).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(true);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DashboardSettingExists(id))
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

        // POST: api/DashboardSettings
        [HttpPost]
        public async Task<ActionResult<DashboardSetting>> PostDashboardSetting(DashboardSetting dashboardSetting)
        {
            if (dashboardSetting == null)
            {
                return BadRequest();
            }

            _context.DashboardSettings.Add(dashboardSetting);
            await _context.SaveChangesAsync().ConfigureAwait(true);

            return CreatedAtAction("GetDashboardSetting", new { id = dashboardSetting.Id }, dashboardSetting);
        }

        // DELETE: api/DashboardSettings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DashboardSetting>> DeleteDashboardSetting(int id)
        {
            var dashboardSetting = await _context.DashboardSettings.FindAsync(id);
            if (dashboardSetting == null)
            {
                return NotFound();
            }

            _context.DashboardSettings.Remove(dashboardSetting);
            await _context.SaveChangesAsync().ConfigureAwait(true);

            return dashboardSetting;
        }

        private bool DashboardSettingExists(int id)
        {
            return _context.DashboardSettings.Any(e => e.Id == id);
        }
    }
}
