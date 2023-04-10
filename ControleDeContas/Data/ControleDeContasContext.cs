using ControleDeContas.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleDeContas.Data
{
    public class ControleDeContasContext : DbContext
    {
        public ControleDeContasContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Conta>? Contas { get; set; }
        public DbSet<Movimentacao>? Movimentacoes { get; set; }
    }
}
