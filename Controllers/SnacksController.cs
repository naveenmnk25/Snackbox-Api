using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SnackboxAPI;
using SnackboxAPI.Models;

[ApiController]
[Route("api/[controller]")]
public class SnacksController : ControllerBase
{
    private readonly ApplicationdbContext _context;

    public SnacksController(ApplicationdbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Snack>>> GetSnacks()
    {
        return await _context.Snacks.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Snack>> GetSnack(int id)
    {
        var snack = await _context.Snacks.FindAsync(id);

        if (snack == null)
        {
            return NotFound();
        }

        return snack;
    }

    [HttpPost]
    public async Task<ActionResult<Snack>> CreateSnack(Snack snack)
    {
        _context.Snacks.Add(snack);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSnack), new { id = snack.Id }, snack);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSnack(int id, Snack snack)
    {
        if (id != snack.Id)
        {
            return BadRequest();
        }

        _context.Entry(snack).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SnackExists(id))
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
    public async Task<IActionResult> DeleteSnack(int id)
    {
        var snack = await _context.Snacks.FindAsync(id);
        if (snack == null)
        {
            return NotFound();
        }

        _context.Snacks.Remove(snack);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool SnackExists(int id)
    {
        return _context.Snacks.Any(e => e.Id == id);
    }
}