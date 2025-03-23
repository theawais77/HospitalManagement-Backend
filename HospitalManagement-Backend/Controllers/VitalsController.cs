using HospitalManagement_Backend.Data;
using HospitalManagement_Backend.DTO;
using HospitalManagement_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VitalsController : ControllerBase
    {
        private readonly HospitalDbContext _dbcontext;
        public VitalsController(HospitalDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vital>>> GetVitals()
        {
            var vitals = await _dbcontext.Vitals
                .Include(v => v.Patient)
                .Include(v => v.Nurse)
                .ToListAsync();
            return Ok(vitals);
        }
        [HttpGet("id")]
        public async Task<ActionResult<Vital>> GetVital(int id)
        {
            var vital = await _dbcontext.Vitals
                .Include(v => v.Patient)
                .Include(v => v.Nurse)
                .FirstOrDefaultAsync(v => v.VitalID == id);

            if (vital == null)
            {
                return NotFound();
            }
            return Ok(vital);
        }
        [HttpPost]
        public async Task<ActionResult<Vital>> AddVital(Vital vital)
        {
            _dbcontext.Vitals.Add(vital);
            await _dbcontext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetVitals), new { id = vital.VitalID }, vital);
        }
        [HttpPut("id")]
        public async Task<ActionResult<Vital>> UpdateVital(int id, Vital vital)
        {
            if (id != vital.VitalID)
            {
                return BadRequest();
            }
            _dbcontext.Entry(vital).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Vital>> DeleteVital(int id)
        {
            var vital = await _dbcontext.Vitals.FindAsync(id);
            if (vital == null)
            {
                return NotFound();
            }
            _dbcontext.Vitals.Remove(vital);
            await _dbcontext.SaveChangesAsync();
            return NoContent();
        }
    }
}
