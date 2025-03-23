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
            var prescriptions = await _dbcontext.Prescriptions.ToListAsync();
            return Ok(prescriptions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Prescription>> GetPrescription(int id)
        {
            var prescription = await _dbcontext.Prescriptions.FindAsync(id);
            if (prescription == null)
            {
                return NotFound();
            }
            return Ok(prescription);
        }
        [HttpPost]
        public async Task<ActionResult<Prescription>> AddPrescription(Prescription prescription)
        {
            _dbcontext.Prescriptions.Add(prescription);
            await _dbcontext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPrescription), new { id = prescription.PrescriptionID }, prescription);
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
