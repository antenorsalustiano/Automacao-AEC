using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prova.Domain.Entities;

namespace Prova.Infra.Data.Mapping
{
    public class CursoMap : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.ToTable("Curso");
            builder.HasKey(p => p.idCurso);

            builder.Property(p => p.Professor);
            builder.Property(p => p.Descricao);
            builder.Property(p => p.CargaHoraria);
        }
    }
}
