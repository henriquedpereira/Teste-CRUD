
using RepositoryHelpers.DataBaseRepository;
using Core.Interfaces.Repository;
using Core.Entities;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;

namespace Infrastructure.Repositories
{
    public class AssuntoRepository : IAssuntoRepository
    {
        private readonly DataContext _context;

        public AssuntoRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Assunto> BuscaAsync(decimal id)
        {
            var customQuery = "Select * from Assunto where CodAs = @codAs";
            var parameters = new { codAs = id };
            var result = await _context.UnitOfWork.Connection.QueryFirstOrDefaultAsync<Assunto>(customQuery, parameters, _context.UnitOfWork.Transaction);
            return result;
        }

        public async Task CriarAsync(Assunto item)
        {
            var query = "INSERT INTO Assunto (Descricao) VALUES (@Descricao);";
            var parameters = new { Descricao = item.Descricao };
            await _context.UnitOfWork.Connection.ExecuteAsync(query, parameters, _context.UnitOfWork.Transaction);
        }

        public async Task AtualizarAsync(Assunto item)
        {
            var query = "UPDATE Assunto SET Descricao = @Descricao WHERE CodAs = @CodAs";
            var parameters = new { Descricao = item.Descricao, CodAs = item.CodAs };
            await _context.UnitOfWork.Connection.ExecuteAsync(query, parameters, _context.UnitOfWork.Transaction);
        }

        public async Task RemoverAsync(decimal id)
        {
            var query = "DELETE FROM Assunto WHERE CodAs = @CodAs";
            var parameters = new { CodAs = id };
            await _context.UnitOfWork.Connection.ExecuteAsync(query, parameters, _context.UnitOfWork.Transaction);
        }

        public async Task<IEnumerable<Assunto>> ListarTodosAsync()
        {
            var query = "SELECT * FROM Assunto";
            return await _context.UnitOfWork.Connection.QueryAsync<Assunto>(query, transaction: _context.UnitOfWork.Transaction);
        }

    }
}