using HospitalManagement_Backend.Data;
using HospitalManagement_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicationsController : Controller
    {
        private readonly HospitalDbContext _dbcontext;

        public MedicationsController(HospitalDbContext dbContext)
        {
            _dbcontext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        // GET: api/Medications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medication>>> GetMedications()
        {
            return await _dbcontext.Medications.ToListAsync();
        }

        // GET: api/Medications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Medication>> GetMedication(int id)
        {
            var medication = await _dbcontext.Medications.FindAsync(id);

            if (medication == null)
            {
                return NotFound();
            }

            return medication;
        }

        // POST: api/Medications
        [HttpPost]
        public async Task<ActionResult<Medication>> PostMedication(Medication medication)
        {
            _dbcontext.Medications.Add(medication);
            await _dbcontext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMedication), new { id = medication.MedicationID }, medication);
        }

        // PUT: api/Medications/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedication(int id, Medication medication)
        {
            if (id != medication.MedicationID)
            {
                return BadRequest();
            }

            _dbcontext.Entry(medication).State = EntityState.Modified;

            try
            {
                await _dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicationExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Medications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedication(int id)
        {
            var medication = await _dbcontext.Medications.FindAsync(id);
            if (medication == null)
            {
                return NotFound();
            }

            _dbcontext.Medications.Remove(medication);
            await _dbcontext.SaveChangesAsync();

            return NoContent();
        }

        private bool MedicationExists(int id)
        {
            return _dbcontext.Medications.Any(e => e.MedicationID == id);
        }
    }
}
