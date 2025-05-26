using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GuiaPlus.API.Models
{
    public class ClienteEndereco
    {
        [Key]
        [Column("tbEnderecosClientes_Id")]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Cliente")]
        [Column("tbClientes_Id")]
        public int ClienteId { get; set; }

        [Required]
        [MaxLength(9)]
        [Column("tbEnderecosClientes_CEP")]
        public required string CEP { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("tbEnderecosClientes_Logradouro")]
        public required string Logradouro { get; set; }

        [Required]
        [MaxLength(60)]
        [Column("tbEnderecosClientes_Bairro")]
        public required string Bairro { get; set; }

        [Required]
        [MaxLength(60)]
        [Column("tbEnderecosClientes_Cidade")]
        public required string Cidade { get; set; }

        [Required]
        [MaxLength(2)]
        [Column("tbEnderecosClientes_UF")]
        public required string UF { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("tbEnderecosClientes_Complemento")]
        public required string Complemento { get; set; }

        [Required]
        [MaxLength(10)]
        [Column("tbEnderecosClientes_Numero")]
        public required string Numero { get; set; }

        [Required]
        [Column("tbEnderecosClientes_Latitude")]
        public decimal Latitude { get; set; }

        [Required]
        [Column("tbEnderecosClientes_Longitude")]
        public decimal Longitude { get; set; }

        [JsonIgnore]
        public virtual Cliente? Cliente { get; set; }
    }
}
