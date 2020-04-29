using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JiraCloneBackend.Data;
using JiraCloneBackend.Models;
using JiraCloneBackend.Helpers;
using System.Text;
using System.Net.Http.Headers;

namespace JiraCloneBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly JiraContext _context;

        public UsersController(JiraContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            users.WithoutPasswords();
            return users;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            //var authHeaderRaw = HttpContext.Request.Headers["Authorization"];
            //var authHeader = AuthenticationHeaderValue.Parse(authHeaderRaw);
            //string encodedAuthParams = authHeader.Parameter;
            //byte[] base64AuthStr = Convert.FromBase64String(encodedAuthParams);
            //string authStr = Encoding.UTF8.GetString(base64AuthStr);
            //string[] authCredentials = authStr.Split(':', 2);
            
            var user = await _context.Users.FindAsync(id);

            
            
            if (user == null)
            {
                return NotFound();
            }

            //if (user.comparePassword(authCredentials[1]))
            //{
            //    return user;
            //} else
            //{
            //    return user.WithoutPassword();
            //}

            return user;

            
            //tested - passed
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserHelper user)
        {
            User saveUser = new User();
            saveUser.Username = user.Username;
            saveUser.FirstName = user.FirstName;
            saveUser.LastName = user.LastName;
            saveUser.Email = user.Email;
            saveUser.generateSaltAndPassword(user.Password);
            _context.Users.Add(saveUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = saveUser.UserId }, saveUser);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
