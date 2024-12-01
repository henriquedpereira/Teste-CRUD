using Core.Entities;
using Core.Interfaces.Repository;
using Dapper;

namespace Infrastructure.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        private readonly DataContext _context;

        public LivroRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Livro> BuscaAsync(decimal id)
        {
            var customQuery = "Select * from Livro where CodL = @codL";
            var parameters = new { codL = id };
            var result = await _context.UnitOfWork.Connection.QueryFirstOrDefaultAsync<Livro>(customQuery, parameters, _context.UnitOfWork.Transaction);
            return result;
        }

        public async Task<int> CriarAsync(Livro item)
        {
            var query = @"
            INSERT INTO Livro (Titulo, AnoPublicacao, Edicao, Editora) 
            OUTPUT INSERTED.CodL 
            VALUES (@Titulo, @AnoPublicacao, @Edicao, @Editora);";
            var parameters = new { item.Titulo, item.AnoPublicacao, item.Edicao, item.Editora };
            var id = await _context.UnitOfWork.Connection.ExecuteScalarAsync<int>(query, parameters, _context.UnitOfWork.Transaction);
            return id;
        }

        public async Task AtualizarAsync(Livro item)
        {
            var query = @"UPDATE Livro SET 
                        Titulo = @Titulo, AnoPublicacao = @AnoPublicacao, Edicao = @Edicao, Editora = @Editora
                        WHERE CodL = @CodL";
            var parameters = new { item.Titulo, item.AnoPublicacao, item.Edicao, item.Editora, item.CodL };
            await _context.UnitOfWork.Connection.ExecuteAsync(query, parameters, _context.UnitOfWork.Transaction);
        }

        public async Task RemoverAsync(decimal id)
        {
            var query = "DELETE FROM Livro WHERE CodL = @CodL";
            var parameters = new { CodL = id };
            await _context.UnitOfWork.Connection.ExecuteAsync(query, parameters, _context.UnitOfWork.Transaction);
        }

        public async Task<IEnumerable<Livro>> ListarTodosAsync()
        {
            var query = "SELECT * FROM Livro";
            return await _context.UnitOfWork.Connection.QueryAsync<Livro>(query, transaction: _context.UnitOfWork.Transaction);
        }

        public async Task<IEnumerable<Livro>> ListarTodosRelatorioAsync()
        {
            var query = "SELECT * FROM Vw_Livro";
            return await _context.UnitOfWork.Connection.QueryAsync<Livro>(query, transaction: _context.UnitOfWork.Transaction);
        }

        public async Task AdicionarAssuntoAsync(LivroAssunto item)
        {
            var query = "INSERT INTO LivroAssunto (LivroCodL,AssuntoCodAs) VALUES (@LivroCodL,@AssuntoCodAs);";
            var parameters = new { item.LivroCodL, item.AssuntoCodAs };
            await _context.UnitOfWork.Connection.ExecuteAsync(query, parameters, _context.UnitOfWork.Transaction);
        }
        public async Task RemoverAssuntoAsync(decimal id)
        {
            var query = "DELETE FROM LivroAssunto WHERE LivroCodL = @id";
            var parameters = new { id };
            await _context.UnitOfWork.Connection.ExecuteAsync(query, parameters, _context.UnitOfWork.Transaction);
        }

        public async Task<IEnumerable<LivroAssunto>> ListarAssuntoAsync(decimal id)
        {
            var query = @"
            SELECT 
                la.LivroCodL, 
                la.AssuntoCodAs, 
                l.Titulo AS Livro, 
                a.Descricao AS Assunto
            FROM 
                LivroAssunto la
            INNER JOIN 
                Livro l ON la.LivroCodL = l.CodL
            INNER JOIN 
                Assunto a ON la.AssuntoCodAs = a.CodAs
            WHERE 
                la.LivroCodL = @CodL";

            var parameters = new { CodL = id };
            return await _context.UnitOfWork.Connection.QueryAsync<LivroAssunto>(query, parameters, _context.UnitOfWork.Transaction);
        }

        public async Task AdicionarAutorAsync(LivroAutor item)
        {
            var query = "INSERT INTO LivroAutor (LivroCodL,AutorCodAu) VALUES (@LivroCodL,@AutorCodAu);";
            var parameters = new { item.LivroCodL, item.AutorCodAu };
            await _context.UnitOfWork.Connection.ExecuteAsync(query, parameters, _context.UnitOfWork.Transaction);
        }

        public async Task RemoverAutorAsync(decimal id)
        {
            var query = "DELETE FROM LivroAutor WHERE LivroCodL = @id";
            var parameters = new { id };
            await _context.UnitOfWork.Connection.ExecuteAsync(query, parameters, _context.UnitOfWork.Transaction);
        }

        public async Task<IEnumerable<LivroAutor>> ListarAutorAsync(decimal id)
        {
            var query = @"
            SELECT 
                la.LivroCodL, 
                la.AutorCodAu, 
                l.Titulo AS Livro, 
                a.Nome AS Autor
            FROM 
                LivroAutor la
            INNER JOIN 
                Livro l ON la.LivroCodL = l.CodL
            INNER JOIN 
                Autor a ON la.AutorCodAu = a.CodAu
            WHERE 
                la.LivroCodL = @CodL";

            var parameters = new { CodL = id };
            return await _context.UnitOfWork.Connection.QueryAsync<LivroAutor>(query, parameters, _context.UnitOfWork.Transaction);
        }

        public async Task AdicionarFormaPagAsync(LivroFormaPag item)
        {
            var query = "INSERT INTO LivroFormaPag (LivroCodL,FormaPagCodForm, Valor) VALUES (@LivroCodL,@FormaPagCodForm,@Valor);";
            var parameters = new { item.LivroCodL, item.FormaPagCodForm, item.Valor };
            await _context.UnitOfWork.Connection.ExecuteAsync(query, parameters, _context.UnitOfWork.Transaction);
        }

        public async Task RemoverFormaPagAsync(decimal id)
        {
            var query = "DELETE FROM LivroFormaPag WHERE LivroCodL = @id";
            var parameters = new { id };
            await _context.UnitOfWork.Connection.ExecuteAsync(query, parameters, _context.UnitOfWork.Transaction);
        }
        public async Task<IEnumerable<LivroFormaPag>> ListarFormaPagAsync(decimal id)
        {
            var query = @"
            SELECT 
                la.LivroCodL, 
                la.FormaPagCodForm, 
                la.Valor,
                l.Titulo AS Livro, 
                fp.Descricao AS Forma_Pag
            FROM 
                LivroFormaPag la
            INNER JOIN 
                Livro l ON la.LivroCodL = l.CodL
            INNER JOIN 
                FormaPag fp ON la.FormaPagCodForm = fp.CodForm
            WHERE 
                la.LivroCodL = @CodL";

            var parameters = new { CodL = id };
            return await _context.UnitOfWork.Connection.QueryAsync<LivroFormaPag>(query, parameters, _context.UnitOfWork.Transaction);
        }


    }
}