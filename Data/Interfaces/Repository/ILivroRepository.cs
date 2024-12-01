
using Core.Entities;

namespace Core.Interfaces.Repository
{
    public interface ILivroRepository
    {
        Task<int> CriarAsync(Livro item);
        Task RemoverAsync(decimal id);
        Task AtualizarAsync(Livro item);
        Task<Livro> BuscaAsync(decimal id);
        Task<IEnumerable<Livro>> ListarTodosAsync();
        Task<IEnumerable<Livro>> ListarTodosRelatorioAsync();

        Task AdicionarAssuntoAsync(LivroAssunto item);
        Task RemoverAssuntoAsync(decimal id);
        Task<IEnumerable<LivroAssunto>> ListarAssuntoAsync(decimal id);

        Task AdicionarAutorAsync(LivroAutor item);
        Task RemoverAutorAsync(decimal id);
        Task<IEnumerable<LivroAutor>> ListarAutorAsync(decimal id);

        Task AdicionarFormaPagAsync(LivroFormaPag item);
        Task RemoverFormaPagAsync(decimal id);
        Task<IEnumerable<LivroFormaPag>> ListarFormaPagAsync(decimal id);

    }
}