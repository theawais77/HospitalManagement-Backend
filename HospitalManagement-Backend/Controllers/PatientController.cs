using HospitalManagement_Backend.Data;
using HospitalManagement_Backend.DTO;
using HospitalManagement_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly HospitalDbContext _dbcontext;
        public PatientController(HospitalDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatient()
        {
            var patients = await _dbcontext.Patients.ToListAsync();
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
            var patient = await _dbcontext.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }
        [HttpPost]
        public async Task<ActionResult<Patient>> AddPatient(Patient patient)
        {
            _dbcontext.Patients.Add(patient);
            await _dbcontext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPatient), new { id = patient.PatientID }, patient);
        }
        [HttpPut("id")]
        public async Task<ActionResult<Patient>> UpdatePatient(int id, Patient patient)
        {
            if (id != patient.PatientID)
            {
                return BadRequest();
            }
            _dbcontext.Entry(patient).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Patient>> DeletePatient(int id)
        {
            var patient = await _dbcontext.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            _dbcontext.Patients.Remove(patient);
            await _dbcontext.SaveChangesAsync();
            return NoContent();
        }
    }
}

