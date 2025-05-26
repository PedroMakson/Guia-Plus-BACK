using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GuiaPlus.API.Enums;

namespace GuiaPlus.API.Models
{
    public class Guia
    {
        [Key]
        [Column("tbGuias_Id")]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Cliente")]
        [Column("tbClientes_Id")]
        public int ClienteId { get; set; }

        [Required]
        [ForeignKey("Servico")]
        [Column("tbServicos_Id")]
        public int ServicoId { get; set; }

        [Required]
        [ForeignKey("ClienteEndereco")]
        [Column("tbEnderecosClientes_Id")]
        public int ClienteEnderecoId { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("tbGuias_NumeroGuia")]
        public string NumeroGuia { get; set; } = null!;

        [Required]
        [Column("tbGuias_Status")]
        public StatusGuia Status { get; set; }

        [Required]
        [Column("tbGuias_DataHoraRegistro")]
        public DateTime DataHoraRegistro { get; set; }

        [Column("tbGuias_DataHoraIniciouColeta")]
        public DateTime? DataHoraIniciouColeta { get; set; }

        [Column("tbGuias_DataHoraConfirmouRetirada")]
        public DateTime? DataHoraConfirmouRetirada { get; set; }

        public virtual Cliente? Cliente { get; set; }
        public virtual Servico? Servico { get; set; }
        public virtual ClienteEndereco? ClienteEndereco { get; set; }
    }
}
