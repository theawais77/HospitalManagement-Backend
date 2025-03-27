using HospitalManagement_Backend.Data;
using HospitalManagement_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordsController : Controller
    {
        private readonly HospitalDbContext _dbcontext;

        public MedicalRecordsController(HospitalDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        // GET: api/MedicalRecords
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<MedicalRecord>>> GetMedicalRecords()
        {
           var medicalRecords = await _dbcontext.MedicalRecords
                .Include(m => m.Patient)
                .Include(m => m.Doctor)
                .ToListAsync();
            return Ok(medicalRecords);
        }

        // GET: api/MedicalRecords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalRecord>> GetMedicalRecord(int id)
        {
            var medicalRecords = await _dbcontext.MedicalRecords
                .Include(m => m.Patient)
                .Include(m => m.Doctor)
                .FirstOrDefaultAsync(m => m.RecordID == id);


            if (medicalRecords == null)
            { 
                return NotFound();
        }

            return medicalRecords;

        }
        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<IEnumerable<MedicalRecord>>> GetMedicalRecordsByPatient(int patientId)
        {
            var records = await _dbcontext.MedicalRecords
                .Include(m => m.Patient)
                .Where(m => m.PatientID == patientId)
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();

            return Ok(records);
        }

        // POST: api/MedicalRecords
        [HttpPost]
        public async Task<ActionResult<MedicalRecord>> PostMedicalRecord(MedicalRecord medicalRecord)
        {
            _dbcontext.MedicalRecords.Add(medicalRecord);
            await _dbcontext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMedicalRecord), new { id = medicalRecord.RecordID }, medicalRecord);
        }

        // PUT: api/MedicalRecords/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicalRecord(int id, MedicalRecord medicalRecord)
        {
            if (id != medicalRecord.RecordID)
            {
                return BadRequest();
            }

            _dbcontext.Entry(medicalRecord).State = EntityState.Modified;

            try
            {
                await _dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicalRecordExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/MedicalRecords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicalRecord(int id)
        {
            var medicalRecord = await _dbcontext.MedicalRecords.FindAsync(id);
            if (medicalRecord == null)
            {
                return NotFound();
            }
        
            _dbcontext.MedicalRecords.Remove(medicalRecord);
            await _dbcontext.SaveChangesAsync();

            return NoContent();
        }

        private bool MedicalRecordExists(int id)
        {
            return _dbcontext.MedicalRecords.Any(e => e.RecordID == id);
        }
    }
}
