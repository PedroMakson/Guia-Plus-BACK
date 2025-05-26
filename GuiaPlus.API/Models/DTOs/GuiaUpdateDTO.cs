using GuiaPlus.API.Enums;

namespace GuiaPlus.API.DTOs
{
    public class GuiaUpdateDTO
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int ServicoId { get; set; }
        public int ClienteEnderecoId { get; set; }
        public string NumeroGuia { get; set; } = string.Empty;
        public StatusGuia Status { get; set; }
        public DateTime? DataHoraIniciouColeta { get; set; }
        public DateTime? DataHoraConfirmouRetirada { get; set; }
    }
}
