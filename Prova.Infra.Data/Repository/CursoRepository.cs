using Prova.Domain.Entities;
using Prova.Domain.Interface;

namespace Prova.Infra.Data.Repository
{
    public class CursoRepository : ICursoRepository
    {
        protected readonly SqlContext _context;

        public CursoRepository(SqlContext context)
        {
            _context = context;
        }

        public void Insert(Curso obj)
        {
            _context.Curso.Add(obj);
            _context.SaveChanges();
        }
    }
}
