using GuiaPlus.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GuiaPlus.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<Guia> Guias { get; set; } // Adicionado
        public DbSet<ClienteEndereco> ClienteEnderecos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().ToTable("tbClientes");
            modelBuilder.Entity<Servico>().ToTable("tbServicos");
            modelBuilder.Entity<ClienteEndereco>().ToTable("tbEnderecosClientes");
            modelBuilder.Entity<Guia>().ToTable("tbGuias");


            // Configura a FK Guia -> Cliente para não excluir em cascata
            modelBuilder.Entity<Guia>()
                .HasOne(g => g.Cliente)
                .WithMany() // sem propriedade de navegação em Cliente
                .HasForeignKey(g => g.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configura a FK Guia -> ClienteEndereco para não excluir em cascata
            modelBuilder.Entity<Guia>()
                .HasOne(g => g.ClienteEndereco)
                .WithMany() // sem propriedade de navegação em ClienteEndereco
                .HasForeignKey(g => g.ClienteEnderecoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configura a FK Guia -> Servico para não excluir em cascata
            modelBuilder.Entity<Guia>()
                .HasOne(g => g.Servico)
                .WithMany() // sem propriedade de navegação em Servico
                .HasForeignKey(g => g.ServicoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configura a FK ClienteEndereco -> Cliente para não excluir em cascata
            modelBuilder.Entity<ClienteEndereco>()
                .HasOne(ce => ce.Cliente)
                .WithMany() // sem propriedade de navegação em Cliente
                .HasForeignKey(ce => ce.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
