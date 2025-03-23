using HospitalManagement_Backend.Data;
using HospitalManagement_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NurseAssignmentsController : Controller
    {
        private readonly HospitalDbContext _dbcontext;

        public NurseAssignmentsController(HospitalDbContext _dbContext)
        {
            _dbcontext = _dbContext;
        }

        // GET: api/NurseAssignments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NurseAssignment>>> GetNurseAssignments()
        {
            var nurseAssignments = await _dbcontext.NurseAssignments
                .Include(na => na.Nurse)   // Include Nurse details
                .Include(na => na.Patient) // Include Patient details
                .ToListAsync();

            return Ok(nurseAssignments);
        }

        // GET: api/NurseAssignments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NurseAssignment>> GetNurseAssignment(int id)
        {
            var nurseAssignment = await _dbcontext.NurseAssignments
                .Include(na => na.Nurse)   // Include Nurse details
                .Include(na => na.Patient) // Include Patient details
                .FirstOrDefaultAsync(na => na.AssignmentID == id);

            if (nurseAssignment == null)
            {
                return NotFound();
            }

            return Ok(nurseAssignment);
        }

        // POST: api/NurseAssignments
        [HttpPost]
        public async Task<ActionResult<NurseAssignment>> CreateNurseAssignment(NurseAssignment nurseAssignment)
        {
            _dbcontext.NurseAssignments.Add(nurseAssignment);
            await _dbcontext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetNurseAssignment), new { id = nurseAssignment.AssignmentID }, nurseAssignment);
        }

        // PUT: api/NurseAssignments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNurseAssignment(int id, NurseAssignment nurseAssignment)
        {
            if (id != nurseAssignment.AssignmentID)
            {
                return BadRequest();
            }

            _dbcontext.Entry(nurseAssignment).State = EntityState.Modified;

            try
            {
                await _dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NurseAssignmentExists(id))
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }

        // DELETE: api/NurseAssignments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNurseAssignment(int id)
        {
            var nurseAssignment = await _dbcontext.NurseAssignments.FindAsync(id);
            if (nurseAssignment == null)
            {
                return NotFound();
            }

            _dbcontext.NurseAssignments.Remove(nurseAssignment);
            await _dbcontext.SaveChangesAsync();
            return NoContent();
        }

        private bool NurseAssignmentExists(int id)
        {
            return _dbcontext.NurseAssignments.Any(e => e.AssignmentID == id);
        }
    }
}
