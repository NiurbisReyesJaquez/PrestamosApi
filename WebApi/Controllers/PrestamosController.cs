using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.DAL;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamosController : ControllerBase
    {
        private readonly Contexto _context;
       
        public PrestamosController(Contexto context)
        {
            _context = context;
           
        }

        // GET: api/Prestamos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prestamos>>> GetPrestamos()
        {
            return await _context.Prestamos.Include(p=> p.Monto).ToListAsync();
        }

        // GET: api/Prestamos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Prestamos>> GetPrestamos(int id)
        {
            var prestamos = await _context.Prestamos.Include(p=> p.Monto).Where(p => p.PrestamosId == id).FirstOrDefaultAsync();


            if (prestamos == null)
            {
                return NotFound();
            }

            return prestamos;
        }

        // PUT: api/Prestamos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrestamos(int id, Prestamos prestamos)
        {
            if (id != prestamos.PrestamosId)
            {
                return BadRequest();
            }

            _context.Entry(prestamos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrestamosExists(id))
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

        // POST: api/Prestamos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Prestamos>> PostPrestamos(Prestamos prestamos)
        {
            _context.Prestamos.Add(prestamos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrestamos", new { id = prestamos.PrestamosId }, prestamos);
        }

        // DELETE: api/Prestamos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Prestamos>> DeletePrestamos(int id)
        {
            var prestamos = await _context.Prestamos.FindAsync(id);
            if (prestamos == null)
            {
                return NotFound();
            }

            _context.Prestamos.Remove(prestamos);

           bool paso = await _context.SaveChangesAsync() > 0;
            if (paso)
            {
                
            }

            return prestamos;
        }
     
       

        private bool PrestamosExists(int id)
        {
            return _context.Prestamos.Any(e => e.PrestamosId == id);
        }
    }
}
