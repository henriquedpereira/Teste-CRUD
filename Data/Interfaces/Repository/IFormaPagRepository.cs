
using Core.Entities;

namespace Core.Interfaces.Repository
{
    public interface IFormaPagRepository
    {
        Task CriarAsync(FormaPag item);
        Task RemoverAsync(decimal id);
        Task AtualizarAsync(FormaPag item);
        Task<FormaPag> BuscaAsync(decimal id);
        Task<IEnumerable<FormaPag>> ListarTodosAsync();
    }
}