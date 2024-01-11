using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SnackboxAPI.Models;

namespace SnackboxAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DrinksController : ControllerBase
    {
        private readonly ApplicationdbContext _context;

        public DrinksController(ApplicationdbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Drink>>> GetDrinks()
        {
            return await _context.Drinks.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Drink>> GetDrink(int id)
        {
            var Drink = await _context.Drinks.FindAsync(id);

            if (Drink == null)
            {
                return NotFound();
            }

            return Drink;
        }

        [HttpPost]
        public async Task<ActionResult<Drink>> CreateDrink(Drink Drink)
        {
            _context.Drinks.Add(Drink);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDrink), new { id = Drink.Id }, Drink);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDrink(int id, Drink Drink)
        {
            if (id != Drink.Id)
            {
                return BadRequest();
            }

            _context.Entry(Drink).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DrinkExists(id))
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
        public async Task<IActionResult> DeleteDrink(int id)
        {
            var Drink = await _context.Drinks.FindAsync(id);
            if (Drink == null)
            {
                return NotFound();
            }

            _context.Drinks.Remove(Drink);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DrinkExists(int id)
        {
            return _context.Drinks.Any(e => e.Id == id);
        }
    }
}