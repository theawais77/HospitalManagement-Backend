using HospitalManagement_Backend.Data;
using HospitalManagement_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicationScheduleController : ControllerBase
    {
        private readonly HospitalDbContext _dbcontext;

        public MedicationScheduleController(HospitalDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        // GET: api/MedicationSchedule
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicationSchedule>>> GetMedicationSchedules()
        {

            var medicationSchedules = await _dbcontext.MedicationSchedule
      .Include(ms => ms.Prescription)
          .ThenInclude(p => p!.Patient)  // Include Patient
      .Include(ms => ms.Prescription)
          .ThenInclude(p => p!.Medication)  // Include Medication
      .Include(ms => ms.Nurse)  // Include Nurse
      .ToListAsync();  // Use ToListAsync()


            return Ok(medicationSchedules);
        }

        // GET: api/MedicationSchedule/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicationSchedule>> GetMedicationSchedule(int id)
        {
            var medicationSchedule = await _dbcontext.MedicationSchedule
      .Include(ms => ms.Prescription)
          .ThenInclude(p => p!.Patient)  // Include Patient
      .Include(ms => ms.Prescription)
          .ThenInclude(p => p!.Medication)  // Include Medication
      .Include(ms => ms.Nurse)  // Include Nurse
                .FirstOrDefaultAsync(ms => ms.ScheduleID == id);

            if (medicationSchedule == null)
            {
                return NotFound();
            }

            return Ok(medicationSchedule);
        }

        // POST: api/MedicationSchedule
        [HttpPost]
        public async Task<ActionResult<MedicationSchedule>> CreateMedicationSchedule(MedicationSchedule medicationSchedule)
        {
            _dbcontext.MedicationSchedule.Add(medicationSchedule);
            await _dbcontext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMedicationSchedule), new { id = medicationSchedule.ScheduleID }, medicationSchedule);
        }

        // PUT: api/MedicationSchedule/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMedicationSchedule(int id, MedicationSchedule medicationSchedule)
        {
            if (id != medicationSchedule.ScheduleID)
            {
                return BadRequest();
            }

            _dbcontext.Entry(medicationSchedule).State = EntityState.Modified;

            try
            {
                await _dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicationScheduleExists(id))
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }

        // DELETE: api/MedicationSchedule/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicationSchedule(int id)
        {
            var medicationSchedule = await _dbcontext.MedicationSchedule.FindAsync(id);
            if (medicationSchedule == null)
            {
                return NotFound();
            }

            _dbcontext.MedicationSchedule.Remove(medicationSchedule);
            await _dbcontext.SaveChangesAsync();
            return NoContent();
        }

        private bool MedicationScheduleExists(int id)
        {
            return _dbcontext.MedicationSchedule.Any(e => e.ScheduleID == id);
        }
    }
}
