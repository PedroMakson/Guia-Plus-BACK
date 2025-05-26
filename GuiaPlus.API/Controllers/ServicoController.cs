using GuiaPlus.API.Data;
using GuiaPlus.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GuiaPlus.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ServicoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var servicos = await _context.Servicos.ToListAsync();
            return Ok(servicos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var servico = await _context.Servicos.FindAsync(id);
            if (servico == null) return NotFound();

            return Ok(servico);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Servico servico)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _context.Servicos.Add(servico);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = servico.Id }, servico);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Servico servico)
        {
            if (id != servico.Id) return BadRequest();

            _context.Entry(servico).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ServicoExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var servico = await _context.Servicos.FindAsync(id);
            if (servico == null) return NotFound();

            _context.Servicos.Remove(servico);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> ServicoExists(int id) => await _context.Servicos.AnyAsync(s => s.Id == id);
    }
}