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

    }
}