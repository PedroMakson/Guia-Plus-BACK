using GuiaPlus.API.Data;
using GuiaPlus.API.DTOs;
using GuiaPlus.API.Enums;
using GuiaPlus.API.Models;
using GuiaPlus.API.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GuiaPlus.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuiaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GuiaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var guias = await _context.Guias
                .Include(g => g.Cliente)
                .Include(g => g.Servico)
                .Include(g => g.ClienteEndereco)
                .ToListAsync();

            var dtoList = guias.Select(g => new GuiaDTO
            {
                Id = g.Id,
                ClienteId = g.ClienteId,
                ServicoId = g.ServicoId,
                ClienteEnderecoId = g.ClienteEnderecoId,
                NumeroGuia = g.NumeroGuia,
                Status = g.Status.ToString(),
                DataHoraRegistro = g.DataHoraRegistro,
                DataHoraIniciouColeta = g.DataHoraIniciouColeta,
                DataHoraConfirmouRetirada = g.DataHoraConfirmouRetirada,
                NomeCliente = g.Cliente.NomeCompleto,
                NomeServico = g.Servico.Nome,
                EnderecoCliente = g.ClienteEndereco.Logradouro
            });

            return Ok(dtoList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var guia = await _context.Guias
                .Include(g => g.Cliente)
                .Include(g => g.Servico)
                .Include(g => g.ClienteEndereco)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (guia == null) return NotFound();

            var dto = new GuiaDTO
            {
                Id = guia.Id,
                ClienteId = guia.ClienteId,
                ServicoId = guia.ServicoId,
                ClienteEnderecoId = guia.ClienteEnderecoId,
                NumeroGuia = guia.NumeroGuia,
                Status = guia.Status.ToString(),
                DataHoraRegistro = guia.DataHoraRegistro,
                DataHoraIniciouColeta = guia.DataHoraIniciouColeta,
                DataHoraConfirmouRetirada = guia.DataHoraConfirmouRetirada,
                NomeCliente = guia.Cliente.NomeCompleto,
                NomeServico = guia.Servico.Nome,
                EnderecoCliente = guia.ClienteEndereco.Logradouro
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GuiaCreateDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var guia = new Guia
            {
                ClienteId = dto.ClienteId,
                ServicoId = dto.ServicoId,
                ClienteEnderecoId = dto.ClienteEnderecoId,
                NumeroGuia = dto.NumeroGuia,
                Status = StatusGuia.Novo,
                DataHoraRegistro = DateTime.Now,
                DataHoraIniciouColeta = dto.DataHoraIniciouColeta,
                DataHoraConfirmouRetirada = dto.DataHoraConfirmouRetirada
            };

            _context.Guias.Add(guia);
            await _context.SaveChangesAsync();

            // Recarrega incluindo dados relacionados para preencher o DTO de resposta
            var guiaCompleto = await _context.Guias
                .Include(g => g.Cliente)
                .Include(g => g.Servico)
                .Include(g => g.ClienteEndereco)
                .FirstOrDefaultAsync(g => g.Id == guia.Id);

            var guiaDTO = new GuiaDTO
            {
                Id = guiaCompleto.Id,
                ClienteId = guiaCompleto.ClienteId,
                ServicoId = guiaCompleto.ServicoId,
                ClienteEnderecoId = guiaCompleto.ClienteEnderecoId,
                NumeroGuia = guiaCompleto.NumeroGuia,
                Status = guiaCompleto.Status.ToString(),
                DataHoraRegistro = guiaCompleto.DataHoraRegistro,
                DataHoraIniciouColeta = guiaCompleto.DataHoraIniciouColeta,
                DataHoraConfirmouRetirada = guiaCompleto.DataHoraConfirmouRetirada,
                NomeCliente = guiaCompleto.Cliente.NomeCompleto,
                NomeServico = guiaCompleto.Servico.Nome,
                EnderecoCliente = guiaCompleto.ClienteEndereco.Logradouro
            };

            return CreatedAtAction(nameof(GetById), new { id = guiaDTO.Id }, guiaDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] GuiaUpdateDTO dto)
        {
            if (id != dto.Id) return BadRequest();

            var guia = await _context.Guias.FindAsync(id);
            if (guia == null) return NotFound();

            guia.ClienteId = dto.ClienteId;
            guia.ServicoId = dto.ServicoId;
            guia.ClienteEnderecoId = dto.ClienteEnderecoId;
            guia.NumeroGuia = dto.NumeroGuia;
            guia.Status = dto.Status;

            // Atualiza apenas se houver valor no DTO
            if (dto.DataHoraIniciouColeta.HasValue)
                guia.DataHoraIniciouColeta = dto.DataHoraIniciouColeta;

            if (dto.DataHoraConfirmouRetirada.HasValue)
                guia.DataHoraConfirmouRetirada = dto.DataHoraConfirmouRetirada;

            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var guia = await _context.Guias.FindAsync(id);
            if (guia == null) return NotFound();

            _context.Guias.Remove(guia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> GuiaExists(int id) =>
            await _context.Guias.AnyAsync(g => g.Id == id);
    }
}
