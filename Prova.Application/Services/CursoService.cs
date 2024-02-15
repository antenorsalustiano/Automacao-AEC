using Prova.Domain.Entities;
using Prova.Domain.Interface;


namespace Prova.Application.Services
{
    public class CursoService : ICursoService
    {
        private readonly ICursoRepository _cursoRepository;
        public CursoService(ICursoRepository cursoRepository) { _cursoRepository = cursoRepository; }
        public void Insert(Curso curso)
        {
            _cursoRepository.Insert(curso);
        }
    }
}
