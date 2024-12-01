
using Core.Entities;

namespace Core.Interfaces.Repository
{
    public interface IAssuntoRepository
    {
        Task CriarAsync(Assunto item);
        Task RemoverAsync(decimal id);
        Task AtualizarAsync(Assunto item);
        Task<Assunto> BuscaAsync(decimal id);
        Task<IEnumerable<Assunto>> ListarTodosAsync();
    }
}