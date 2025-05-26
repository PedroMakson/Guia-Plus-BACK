using GuiaPlus.API.Data;
using GuiaPlus.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GuiaPlus.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteEnderecosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClienteEnderecosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var enderecos = await _context.ClienteEnderecos.Include(e => e.Cliente).ToListAsync();
            return Ok(enderecos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var endereco = await _context.ClienteEnderecos.Include(e => e.Cliente).FirstOrDefaultAsync(e => e.Id == id);
            if (endereco == null) return NotFound();

            return Ok(endereco);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClienteEndereco endereco)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _context.ClienteEnderecos.Add(endereco);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = endereco.Id }, endereco);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ClienteEndereco endereco)
        {
            if (id != endereco.Id) return BadRequest();

            _context.Entry(endereco).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ClienteEnderecoExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var endereco = await _context.ClienteEnderecos.FindAsync(id);
            if (endereco == null) return NotFound();

            _context.ClienteEnderecos.Remove(endereco);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> ClienteEnderecoExists(int id) => await _context.ClienteEnderecos.AnyAsync(e => e.Id == id);
    }
}
