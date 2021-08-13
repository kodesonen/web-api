using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Models;

namespace api.Controllers
{
    public interface IModuleController
    {
        Task<ActionResult<IEnumerable<Module>>> Getmodules();
        Task<ActionResult<Module>> GetModule(int id);
        Task<IActionResult> PutModule(int id, Module @module);
        Task<ActionResult<Module>> PostModule(Module @module);
        Task<IActionResult> DeleteModule(int id);
    }


    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController : ControllerBase, IModuleController
    {
        private readonly DataContext _context;

        public ModuleController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Module
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Module>>> Getmodules()
        {
            return await _context.modules.ToListAsync();
        }

        // GET: api/Module/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Module>> GetModule(int id)
        {
            var @module = await _context.modules.FindAsync(id);

            if (@module == null)
            {
                return NotFound();
            }

            return @module;
        }

        // PUT: api/Module/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModule(int id, Module @module)
        {
            if (id != @module.Id)
            {
                return BadRequest();
            }

            _context.Entry(@module).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModuleExists(id))
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

        // POST: api/Module
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Module>> PostModule(Module @module)
        {
            _context.modules.Add(@module);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetModule", new { id = @module.Id }, @module);
            return CreatedAtAction(nameof(GetModule), new { id = module.Id }, module);
        }

        // DELETE: api/Module/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModule(int id)
        {
            var @module = await _context.modules.FindAsync(id);
            if (@module == null)
            {
                return NotFound();
            }

            _context.modules.Remove(@module);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ModuleExists(int id)
        {
            return _context.modules.Any(e => e.Id == id);
        }
    }
}
