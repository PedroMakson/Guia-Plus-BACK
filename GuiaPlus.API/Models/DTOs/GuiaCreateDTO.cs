using GuiaPlus.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace GuiaPlus.API.Models.DTOs;

public class GuiaCreateDTO
{
    [Required]
    public int ClienteId { get; set; }

    [Required]
    public int ServicoId { get; set; }

    [Required]
    public int ClienteEnderecoId { get; set; }

    [Required]
    [MaxLength(20)]
    public string NumeroGuia { get; set; } = null!;

    public DateTime? DataHoraIniciouColeta { get; set; }
    public DateTime? DataHoraConfirmouRetirada { get; set; }
}
