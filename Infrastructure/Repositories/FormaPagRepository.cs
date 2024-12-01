
using RepositoryHelpers.DataBaseRepository;
using Core.Interfaces.Repository;
using Core.Entities;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;

namespace Infrastructure.Repositories
{
    public class FormaPagRepository : IFormaPagRepository
    {
        private readonly DataContext _context;

        public FormaPagRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<FormaPag> BuscaAsync(decimal id)
        {
            var customQuery = "Select * from FormaPag where CodForm = @codForm";
            var parameters = new { codForm = id };
            var result = await _context.UnitOfWork.Connection.QueryFirstOrDefaultAsync<FormaPag>(customQuery, parameters, _context.UnitOfWork.Transaction);
            return result;
        }

        public async Task CriarAsync(FormaPag item)
        {
            var query = "INSERT INTO FormaPag (Descricao) VALUES (@Descricao);";
            var parameters = new { Descricao = item.Descricao };
            await _context.UnitOfWork.Connection.ExecuteAsync(query, parameters, _context.UnitOfWork.Transaction);
        }

        public async Task AtualizarAsync(FormaPag item)
        {
            var query = "UPDATE FormaPag SET Descricao = @Descricao WHERE CodForm = @CodForm";
            var parameters = new { Descricao = item.Descricao, CodForm = item.CodForm };
            await _context.UnitOfWork.Connection.ExecuteAsync(query, parameters, _context.UnitOfWork.Transaction);
        }

        public async Task RemoverAsync(decimal id)
        {
            var query = "DELETE FROM FormaPag WHERE CodForm = @CodForm";
            var parameters = new { CodForm = id };
            await _context.UnitOfWork.Connection.ExecuteAsync(query, parameters, _context.UnitOfWork.Transaction);
        }

        public async Task<IEnumerable<FormaPag>> ListarTodosAsync()
        {
            var query = "SELECT * FROM FormaPag";
            return await _context.UnitOfWork.Connection.QueryAsync<FormaPag>(query, transaction: _context.UnitOfWork.Transaction);
        }

    }
}