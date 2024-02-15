using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Prova.Domain.Entities;
using Prova.Infra.Data.Mapping;

namespace Prova.Infra.Data
{
    public class SqlContext : DbContext
    {
        public SqlContext(DbContextOptions<SqlContext> options) : base(options){}

        public DbSet<Curso> Curso { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Curso>(new CursoMap().Configure);
        }

    }
}