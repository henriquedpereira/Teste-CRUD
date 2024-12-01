
using Core.Entities;

namespace Core.Interfaces.Repository
{
    public interface ILivroRepository : IDisposable
    {
        Task Criar(Livro item);
        Task Atualizar(Livro item);
    }
}