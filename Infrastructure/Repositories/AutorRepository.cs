
using RepositoryHelpers.DataBaseRepository;
using Core.Interfaces.Repository;
using Core.Entities;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;

namespace Infrastructure.Repositories
{
    public class AutorRepository : IAutorRepository
    {
        private readonly DataContext _context;

        public AutorRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Autor> BuscaAsync(decimal id)
        {
            var customQuery = "Select * from Autor where CodAu = @codAu";
            var parameters = new { codAu = id };
            var result = await _context.UnitOfWork.Connection.QueryFirstOrDefaultAsync<Autor>(customQuery, parameters, _context.UnitOfWork.Transaction);
            return result;
        }

        public async Task CriarAsync(Autor item)
        {
            var query = "INSERT INTO Autor (Nome) VALUES (@Nome);";
            var parameters = new { Nome = item.Nome };
            await _context.UnitOfWork.Connection.ExecuteAsync(query, parameters, _context.UnitOfWork.Transaction);
        }

        public async Task AtualizarAsync(Autor item)
        {
            var query = "UPDATE Autor SET Nome = @Nome WHERE CodAu = @CodAu";
            var parameters = new { Nome = item.Nome, CodAu = item.CodAu };
            await _context.UnitOfWork.Connection.ExecuteAsync(query, parameters, _context.UnitOfWork.Transaction);
        }

        public async Task RemoverAsync(decimal id)
        {
            var query = "DELETE FROM Autor WHERE CodAu = @CodAu";
            var parameters = new { CodAu = id };
            await _context.UnitOfWork.Connection.ExecuteAsync(query, parameters, _context.UnitOfWork.Transaction);
        }

        public async Task<IEnumerable<Autor>> ListarTodosAsync()
        {
            var query = "SELECT * FROM Autor";
            return await _context.UnitOfWork.Connection.QueryAsync<Autor>(query, transaction: _context.UnitOfWork.Transaction);
        }

    }
}