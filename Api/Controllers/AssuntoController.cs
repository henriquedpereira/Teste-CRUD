using Core.Entities;
using Core.Entities.Validators;
using Core.Interfaces.Repository;
using FastReport.Export.PdfSimple;
using FastReport.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Polly;
using System.Data;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssuntoController : ControllerBase
    {
        private readonly IAssuntoRepository _assuntoRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AssuntoController(IAssuntoRepository assuntoRepository, IWebHostEnvironment webHostEnvironment)
        {
            _assuntoRepository = assuntoRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("{id}")]
        public async Task<Assunto> BuscaAsync([FromRoute] decimal id)
        {
            return await _assuntoRepository.BuscaAsync(id);
        }

        [HttpGet()]
        public async Task<IEnumerable<Assunto>> ListarTodosAsync()
        {
            return await _assuntoRepository.ListarTodosAsync();
        }

        [HttpPost()]
        public IActionResult Gravar(Assunto model)
        {
            var validator = new AssuntoValidator();
            var result = validator.Validate(model);

            if (!result.IsValid)
                return BadRequest(result.Errors);

            try
            {
                if (model.CodAs != 0)
                    _assuntoRepository.AtualizarAsync(model);
                else
                    _assuntoRepository.CriarAsync(model);
            }
            catch (Exception)
            {
                return BadRequest("Ocorreu um erro, verifique com um administrador.");
            }

            return Ok(new { message = "Item atualizado com sucesso." });
        }

        [HttpDelete("{id}")]
        public IActionResult Remover([FromRoute] decimal id)
        {
            try
            {
                _assuntoRepository.RemoverAsync(id);
            }
            catch (Exception)
            {
                return BadRequest("Ocorreu um erro, verifique com um administrador.");
            }
            return Ok(new { message = "Item removido com sucesso." });
        }

        [Route("report")]
        [HttpGet]
        [ProducesResponseType(typeof(List<Assunto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Assunto>>> Report()
        {
            try
            {
                var list = await _assuntoRepository.ListarTodosAsync();
                if (list is null)
                    return NotFound();

                var webReport = new WebReport();

                var caminho = Path.Combine(_webHostEnvironment.ContentRootPath,
                    "reports", "assuntoReport.frx");
                webReport.Report.Load(caminho);

                GenerateDataTableReport(list.ToList(), webReport);

                webReport.Report.Prepare();

                using MemoryStream stream = new MemoryStream();

                webReport.Report.Export(new PDFSimpleExport(), stream);

                stream.Flush();
                byte[] arrayReport = stream.ToArray();

                return File(arrayReport, "application/zip", "AssuntosReport.pdf");
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void GenerateDataTableReport(List<Assunto> itens, WebReport webReport)
        {
            var listDataTable = new DataTable();
            listDataTable.Columns.Add("CodAs", typeof(int));
            listDataTable.Columns.Add("Descricao", typeof(string));

            foreach (var item in itens)
            {
                listDataTable.Rows.Add(item.CodAs,
                               item.Descricao);
            }
            webReport.Report.RegisterData(listDataTable, "Assuntos");
        }


    }
}