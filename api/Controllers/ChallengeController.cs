using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using api.Models;

namespace api.Controllers
{
    public interface IChallengeController
    {
        Task<ActionResult<IEnumerable<Challenge>>> Getchallenges();
        Task<ActionResult<Challenge>> GetChallenge(int id);
        Task<IActionResult> PutChallenge(int id, Challenge challenge);
        Task<ActionResult<Challenge>> PostChallenge(Challenge challenge);
        Task<IActionResult> DeleteChallenge(int id);
    }


    [Route("api/[controller]")]
    [Authorize(Roles = "Admin, Contributor")]
    [ApiController]
    public class ChallengeController : ControllerBase
    {
        private readonly DataContext _context;

        public ChallengeController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Challenge
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Challenge>>> Getchallenges()
        {
            return await _context.challenges.ToListAsync();
        }

        // GET: api/Challenge/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Challenge>> GetChallenge(int id)
        {
            var challenge = await _context.challenges.FindAsync(id);

            if (challenge == null)
            {
                return NotFound();
            }

            return challenge;
        }

        // PUT: api/Challenge/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChallenge(int id, Challenge challenge)
        {
            if (id != challenge.Id)
            {
                return BadRequest();
            }

            _context.Entry(challenge).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChallengeExists(id))
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

        // POST: api/Challenge
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Challenge>> PostChallenge(Challenge challenge)
        {
            _context.challenges.Add(challenge);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChallenge", new { id = challenge.Id }, challenge);
        }

        // DELETE: api/Challenge/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChallenge(int id)
        {
            var challenge = await _context.challenges.FindAsync(id);
            if (challenge == null)
            {
                return NotFound();
            }

            _context.challenges.Remove(challenge);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChallengeExists(int id)
        {
            return _context.challenges.Any(e => e.Id == id);
        }
    }
}
