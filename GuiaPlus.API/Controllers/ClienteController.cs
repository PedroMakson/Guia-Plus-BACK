using GuiaPlus.API.Data;
using GuiaPlus.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace GuiaPlus.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<Cliente> _passwordHasher;

        public ClienteController(AppDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Cliente>();
        }

        // GET: api/Cliente
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clientes = await _context.Clientes.ToListAsync();
            return Ok(clientes);
        }

        // GET: api/Cliente/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

        // POST: api/Cliente
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Cliente cliente)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Hashear a senha antes de salvar
            cliente.Senha = _passwordHasher.HashPassword(cliente, cliente.Senha);

            // O campo Id será gerado automaticamente pelo banco (auto-increment)
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = cliente.Id }, cliente);
        }

        // PUT: api/Cliente/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Cliente cliente)
        {
            if (id != cliente.Id)
                return BadRequest("O ID da rota e do corpo não conferem.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingCliente = await _context.Clientes.FindAsync(id);
            if (existingCliente == null)
                return NotFound();

            // Atualiza os campos manualmente para evitar problemas com tracking
            existingCliente.CPFCNPJ = cliente.CPFCNPJ;
            existingCliente.NomeCompleto = cliente.NomeCompleto;
            existingCliente.Email = cliente.Email;
            existingCliente.Telefone = cliente.Telefone;
            existingCliente.Status = cliente.Status;

            // Se a senha recebida não for nula ou vazia, faz o hash e atualiza
            if (!string.IsNullOrWhiteSpace(cliente.Senha))
            {
                existingCliente.Senha = _passwordHasher.HashPassword(existingCliente, cliente.Senha);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ClienteExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/Cliente/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
                return NotFound();

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> ClienteExists(int id)
        {
            return await _context.Clientes.AnyAsync(e => e.Id == id);
        }
    }
}