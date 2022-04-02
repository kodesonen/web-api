using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using api.Models.Settings;
using api.Models;

namespace api.Controllers
{
    public interface IUserController
    {
        Task<ActionResult<IEnumerable<User>>> Getusers();
        List<User> GetPublicUsers();
        Task<ActionResult<User>> GetUser(string id);
        User GetUserByUrlName(string name);
        Task<IdentityResult> ChangePassword(EditPasswordModel model);
        Task<IActionResult> PutUser(string id, User user);
        Task<IActionResult> DeleteUser(string id);

    }


    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles="Admin, Contributor")]

    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContext;

        public UserController(DataContext context, UserManager<User> userManager, IHttpContextAccessor httpContext)
        {
            _context = context;
            _userManager = userManager;
            _httpContext = httpContext;

        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Getusers()
        {
            return await _context.users.ToListAsync();
        }

        // GET: api/User/GetPublicUsers
        [HttpGet("[action]")]
        public List<User> GetPublicUsers()
        {
            var Data = _context.Users.Where(x => x.Private == false).OrderByDescending(y => y.Id);
            List<User> ListOfUsers = Data.ToList();
            return ListOfUsers;
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            var user = await _context.users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // GET: api/User/UrlName=MagnusBred
        [HttpGet("UrlName={name}")]
        public User GetUserByUrlName(string name)
        {
            var Data = _context.Users.Where(x => x.UrlName == name).FirstOrDefault();
            if (Data != null) return Data;
            return null;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, User user)
        {
            if (id != user.Id)
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

        // POST: api/User/ChangePassword
        [HttpPost("[action]")]
        [Authorize(Roles="Admin, Contributor, User")]

        public async Task<IdentityResult> ChangePassword(EditPasswordModel model)
        {
            User user = await _userManager.GetUserAsync(_httpContext.HttpContext.User);
            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            return result;
        }


        /*
        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.users.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }
        */

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        [Authorize(Roles="Admin, Contributor, User")]

        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _context.users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(string id)
        {
            return _context.users.Any(e => e.Id == id);
        }
    }
}
