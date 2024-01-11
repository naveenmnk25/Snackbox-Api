using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SnackboxAPI;
using SnackboxAPI.Models;

namespace DessertboxAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DessertsController : ControllerBase
    {
        private readonly ApplicationdbContext _context;

        public DessertsController(ApplicationdbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dessert>>> GetDesserts()
        {
            return await _context.Desserts.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Dessert>> GetDessert(int id)
        {
            var Dessert = await _context.Desserts.FindAsync(id);

            if (Dessert == null)
            {
                return NotFound();
            }

            return Dessert;
        }

        [HttpPost]
        public async Task<ActionResult<Dessert>> CreateDessert(Dessert Dessert)
        {
            _context.Desserts.Add(Dessert);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDessert), new { id = Dessert.Id }, Dessert);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDessert(int id, Dessert Dessert)
        {
            if (id != Dessert.Id)
            {
                return BadRequest();
            }

            _context.Entry(Dessert).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DessertExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDessert(int id)
        {
            var Dessert = await _context.Desserts.FindAsync(id);
            if (Dessert == null)
            {
                return NotFound();
            }

            _context.Desserts.Remove(Dessert);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DessertExists(int id)
        {
            return _context.Desserts.Any(e => e.Id == id);
        }
    }
}