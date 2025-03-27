using HospitalManagement_Backend.Data;
using HospitalManagement_Backend.DTO;
using HospitalManagement_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly HospitalDbContext _dbcontext;
        public PrescriptionController(HospitalDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prescription>>> GetPrescriptions()
        {
            var prescriptions = await _dbcontext.Prescriptions
                .Include(p => p.Patient)
                .Include(p => p.Doctor)
                .Include(p => p.Medication)
                .ToListAsync();

            return Ok(prescriptions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Prescription>> GetPrescription(int id)
        {
            var prescription = await _dbcontext.Prescriptions
                .Include(p => p.Patient)
                .Include(p => p.Doctor)
                .Include(p => p.Medication)
                .FirstOrDefaultAsync(p => p.PrescriptionID == id);

            if (prescription == null) return NotFound();
            return prescription;
        }
        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<IEnumerable<Prescription>>> GetPrescriptionsByPatient(int patientId)
        {
            var prescriptions = await _dbcontext.Prescriptions
                .Include(p => p.Patient)
                .Include(p => p.Medication)
                .Where(p => p.PatientID == patientId)
                .OrderByDescending(p => p.PrescribedAt)
                .ToListAsync();

            return Ok(prescriptions);
        }
        [HttpPost]
        public async Task<ActionResult<Prescription>> CreatePrescription([FromBody] Prescription prescription)
        {
            var newPrescription = new Prescription
            {
                PatientID = prescription.PatientID,
                DoctorID = prescription.DoctorID,
                MedicationID = prescription.MedicationID,
                Dosage = prescription.Dosage,
                Instructions = prescription.Instructions,
                PrescribedAt = prescription.PrescribedAt
            };

            _dbcontext.Prescriptions.Add(newPrescription);
            await _dbcontext.SaveChangesAsync();

            // Load related data
            await _dbcontext.Entry(newPrescription)
                .Reference(p => p.Patient)
                .LoadAsync();
            await _dbcontext.Entry(newPrescription)
                .Reference(p => p.Doctor)
                .LoadAsync();
            await _dbcontext.Entry(newPrescription)
                .Reference(p => p.Medication)
                .LoadAsync();

            return CreatedAtAction(nameof(GetPrescription), new { id = newPrescription.PrescriptionID }, newPrescription);
        }

        [HttpPut("id")]
        public async Task<ActionResult<Prescription>> UpdatePrescription(int id, Prescription prescription)
        {
            if (id != prescription.PrescriptionID)
            {
                return BadRequest();
            }
            _dbcontext.Entry(prescription).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Prescription>> DeletePrescription(int id)
        {
            var prescription = await _dbcontext.Prescriptions.FindAsync(id);
            if (prescription == null)
            {
                return NotFound();
            }
            _dbcontext.Prescriptions.Remove(prescription);
            await _dbcontext.SaveChangesAsync();
            return NoContent();
        }
    }
}
