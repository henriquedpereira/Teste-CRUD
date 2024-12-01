
using Core.Entities;

namespace Core.Interfaces.Repository
{
    public interface IAutorRepository
    {
        Task CriarAsync(Autor item);
        Task RemoverAsync(decimal id);
        Task AtualizarAsync(Autor item);
        Task<Autor> BuscaAsync(decimal id);
        Task<IEnumerable<Autor>> ListarTodosAsync();
    }
}