using Core.Entities;
using Core.Entities.Validators;
using Core.Interfaces.Repository;
using FastReport.Export.PdfSimple;
using FastReport.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Polly;
using System.Data;
using System.Reflection;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivroController : ControllerBase
    {
        private readonly ILivroRepository _livroRepository;
        private readonly IAssuntoRepository _assuntoRepository;
        private readonly IAutorRepository _autorRepository;
        private readonly IFormaPagRepository _formaPagamentoRepository;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public LivroController(ILivroRepository livroRepository, IWebHostEnvironment webHostEnvironment, IAssuntoRepository assuntoRepository, IAutorRepository autorRepository, IFormaPagRepository formaPagamentoRepository)
        {
            _livroRepository = livroRepository;
            _webHostEnvironment = webHostEnvironment;
            _assuntoRepository = assuntoRepository;
            _autorRepository = autorRepository;
            _formaPagamentoRepository = formaPagamentoRepository;
        }

        [HttpGet("{id}")]
        public async Task<Livro> BuscaAsync([FromRoute] decimal id)
        {
            var item = await _livroRepository.BuscaAsync(id);
            item = item ?? new Livro(0, "", "", 0, "");

            var assuntos = await _assuntoRepository.ListarTodosAsync();
            var assuntosSelecionados = await _livroRepository.ListarAssuntoAsync(id);

            item.ListaAssuntos = assuntos.Select(a => new SelectListItem
            {
                text = a.Descricao,
                value = a.CodAs.ToString(),
                selected = assuntosSelecionados.Any(sa => sa.AssuntoCodAs == a.CodAs)
            }).ToList();

            var autores = await _autorRepository.ListarTodosAsync();
            var autoresSelecionados = await _livroRepository.ListarAutorAsync(id);

            item.ListaAutores = autores.Select(a => new SelectListItem
            {
                text = a.Nome,
                value = a.CodAu.ToString(),
                selected = autoresSelecionados.Any(sa => sa.AutorCodAu == a.CodAu)
            }).ToList();

            var formasPagamento = await _formaPagamentoRepository.ListarTodosAsync();
            var formasPagamentoSelecionadas = await _livroRepository.ListarFormaPagAsync(id);

            item.ListaFormasPag = formasPagamento.Select(a => new SelectListItem
            {
                text = a.Descricao,
                value = a.CodForm.ToString(),
                value2 = formasPagamentoSelecionadas.FirstOrDefault(sa => sa.FormaPagCodForm == a.CodForm)?.Valor,
                selected = formasPagamentoSelecionadas.Any(sa => sa.FormaPagCodForm == a.CodForm)
            }).ToList();


            return item;
        }

        [HttpGet()]
        public async Task<IEnumerable<Livro>> ListarTodosAsync()
        {
            return await _livroRepository.ListarTodosAsync();
        }

        [HttpPost()]
        public async Task<IActionResult> Gravar(Livro model)
        {
            var validator = new LivroValidator();
            var result = validator.Validate(model);

            if (!result.IsValid)
                return BadRequest(result.Errors);

            try
            {
                if (model.CodL != 0)
                    await _livroRepository.AtualizarAsync(model);
                else
                    model.CodL = await _livroRepository.CriarAsync(model);

                await _livroRepository.RemoverAssuntoAsync(model.CodL);
                foreach (var item in model.ListaAssuntos)
                {
                    if (item.selected)
                        await _livroRepository.AdicionarAssuntoAsync(new LivroAssunto { LivroCodL = model.CodL, AssuntoCodAs = int.Parse(item.value) });
                }

                await _livroRepository.RemoverAutorAsync(model.CodL);
                foreach (var item in model.ListaAutores)
                {
                    if (item.selected)
                        await _livroRepository.AdicionarAutorAsync(new LivroAutor { LivroCodL = model.CodL, AutorCodAu = int.Parse(item.value) });
                }

                await _livroRepository.RemoverFormaPagAsync(model.CodL);
                foreach (var item in model.ListaFormasPag)
                {
                    if (item.selected)
                    {
                        await _livroRepository.AdicionarFormaPagAsync(new LivroFormaPag { LivroCodL = model.CodL, FormaPagCodForm = int.Parse(item.value), Valor = item.value2 ?? 0 });
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest("Ocorreu um erro, verifique com um administrador.");
            }

            return Ok(new { message = "Item atualizado com sucesso." });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover([FromRoute] decimal id)
        {
            try
            {
                await _livroRepository.RemoverAutorAsync(id);
                await _livroRepository.RemoverAssuntoAsync(id);
                await _livroRepository.RemoverFormaPagAsync(id);
                await _livroRepository.RemoverAsync(id);
            }
            catch (Exception)
            {
                return BadRequest("Ocorreu um erro, verifique com um administrador.");
            }
            return Ok(new { message = "Item removido com sucesso." });
        }

        [Route("report")]
        [HttpGet]
        [ProducesResponseType(typeof(List<Livro>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Livro>>> Report()
        {
            try
            {
                var list = await _livroRepository.ListarTodosRelatorioAsync();
                if (list is null)
                    return NotFound();

                var webReport = new WebReport();

                var caminho = Path.Combine(_webHostEnvironment.ContentRootPath,
                    "reports", "livroReport.frx");
                webReport.Report.Load(caminho);

                GenerateDataTableReport(list.ToList(), webReport);

                webReport.Report.Prepare();

                using MemoryStream stream = new MemoryStream();

                webReport.Report.Export(new PDFSimpleExport(), stream);

                stream.Flush();
                byte[] arrayReport = stream.ToArray();

                return File(arrayReport, "application/zip", "LivrosReport.pdf");
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void GenerateDataTableReport(List<Livro> itens, WebReport webReport)
        {
            var listDataTable = new DataTable();
            listDataTable.Columns.Add("CodL", typeof(int));
            listDataTable.Columns.Add("Titulo", typeof(string));
            listDataTable.Columns.Add("Editora", typeof(string));
            listDataTable.Columns.Add("Edicao", typeof(int));
            listDataTable.Columns.Add("AnoPublicacao", typeof(string));
            listDataTable.Columns.Add("Assuntos", typeof(string));
            listDataTable.Columns.Add("Autores", typeof(string));
            listDataTable.Columns.Add("FormasPagamento", typeof(string));

            foreach (var item in itens)
            {
                listDataTable.Rows.Add(item.CodL, item.Titulo, item.Editora, item.Edicao, item.AnoPublicacao, 
                                        item.Assuntos, item.Autores, item.FormasPagamento);
            }
            webReport.Report.RegisterData(listDataTable, "Livros");
        }


    }
}