
using Core.Entities;

namespace Core.Interfaces.Repository
{
    public interface IAssuntoRepository
    {
        Task Criar(Assunto item);
        Task Atualizar(Assunto item);
        Task<Assunto> Busca(decimal id);
        Task<IEnumerable<Assunto>> ListarTodos();
    }
}