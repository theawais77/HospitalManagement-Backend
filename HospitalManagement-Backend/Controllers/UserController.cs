using HospitalManagement_Backend.Data;
using HospitalManagement_Backend.DTO;
using HospitalManagement_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly HospitalDbContext _dbcontext;
        public UserController(HospitalDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAction()
        {
            var users = await _dbcontext.Users.ToListAsync();
            return Ok(users);
        }
        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostAction(UserDTO userDTO)
        {
            var user = new User
            {
                UserID = userDTO.UserID,
                Username = userDTO.Username,
                PasswordHash = "", // You need to handle password hashing
                FullName = userDTO.FullName,
                Role = userDTO.Role,
                CreatedAt = userDTO.CreatedAt
            };

            _dbcontext.Users.Add(user);
            await _dbcontext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAction), new { id = user.UserID }, userDTO);//for front end to know the location of the created user
        }
    }
}
