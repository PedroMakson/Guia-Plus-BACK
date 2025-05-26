using GuiaPlus.API.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GuiaPlus.API.Models
{
    public class Servico
    {
        [Key]
        [Column("tbServicos_Id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("tbServicos_Nome")]
        public required string Nome { get; set; }

        [Required]
        [Column("tbServicos_Status")]
        public StatusServico Status { get; set; }

    }
}
