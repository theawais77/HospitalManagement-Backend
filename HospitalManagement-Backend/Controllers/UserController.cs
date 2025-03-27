using HospitalManagement_Backend.Data;
using HospitalManagement_Backend.DTO;
using HospitalManagement_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

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

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetAction(int id)
        {
            var user = await _dbcontext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> AddUser(UserDTO userDTO)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);

            var user = new User
            {
                UserID = userDTO.UserID,
                Username = userDTO.Username,
                PasswordHash = hashedPassword,
                FullName = userDTO.FullName,
                Role = userDTO.Role,
                CreatedAt = userDTO.CreatedAt
            };

            _dbcontext.Users.Add(user);
            await _dbcontext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAction), new { id = user.UserID }, userDTO);
        }

        [HttpPut("{id}")] 
        public async Task<ActionResult<UserDTO>> UpdateUser(int id, UserDTO userDTO)
        {
            if (id != userDTO.UserID)
            {
                return BadRequest();
            }
            var user = await _dbcontext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            string hashedPassword = user.PasswordHash;
            if (userDTO.Password != null && !BCrypt.Net.BCrypt.Verify(userDTO.Password, user.PasswordHash))
            {
                hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);
            }

            user.Username = userDTO.Username;
            user.PasswordHash = hashedPassword;
            user.FullName = userDTO.FullName;
            user.Role = userDTO.Role;
            user.CreatedAt = userDTO.CreatedAt;

            await _dbcontext.SaveChangesAsync(); 
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await _dbcontext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            _dbcontext.Users.Remove(user);
            await _dbcontext.SaveChangesAsync();
            return NoContent();
        }
    }
}