namespace GuiaPlus.API.DTOs
{
    public class GuiaDTO
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int ServicoId { get; set; }
        public int ClienteEnderecoId { get; set; }
        public string NumeroGuia { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime DataHoraRegistro { get; set; }
        public DateTime? DataHoraIniciouColeta { get; set; }
        public DateTime? DataHoraConfirmouRetirada { get; set; }

        public string NomeCliente { get; set; } = string.Empty;
        public string NomeServico { get; set; } = string.Empty;
        public string EnderecoCliente { get; set; } = string.Empty;
    }
}
