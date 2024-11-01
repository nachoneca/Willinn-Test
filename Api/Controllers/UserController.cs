using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;
using Data;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _context.User.ToListAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                throw new Exception("The user you are looking for is invalid, please try with another Id!");
                /*return NotFound();*/
            }

            return Ok(user);
        }

        [HttpPost("create")]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            _context.User.Add(user);

            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, User user)
        {
            var us = await _context.User.FindAsync(id);
            if (us == null) 
                return NotFound();
            us.Name = user.Name;
            us.Email = user.Email;
            us.IsActive = user.IsActive;
            await _context.SaveChangesAsync();
            return Ok(user);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                throw new Exception("The user you are looking for is invalid, please try with another Id!");
                /*return NotFound();*/
            }
            user.IsActive = false;
            await _context.SaveChangesAsync();
            Console.WriteLine($"User with ID {id} has been marked as inactive.");
            return Ok(user);
        }
    }
}