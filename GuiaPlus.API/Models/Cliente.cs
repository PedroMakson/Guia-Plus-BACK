using GuiaPlus.API.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GuiaPlus.API.Models
{
    [Index(nameof(CPFCNPJ), IsUnique = true)]
    public class Cliente
    {
        [Key]
        [Column("tbClientes_Id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(18)]
        [Column("tbClientes_CPFCNPJ")]
        public required string CPFCNPJ { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("tbClientes_NomeCompleto")]
        public required string NomeCompleto { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        [Column("tbClientes_Email")]
        public required string Email { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("tbClientes_Telefone")]
        public required string Telefone { get; set; }

        [Required]
        [Column("tbClientes_Status")]
        public StatusCliente Status { get; set; }

        [Required]
        [Column("tbClientes_Senha")]
        public required string Senha { get; set; }
    }
}