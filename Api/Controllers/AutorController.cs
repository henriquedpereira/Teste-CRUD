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
    public class AutorController : ControllerBase
    {
        private readonly IAutorRepository _autorRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AutorController(IAutorRepository autorRepository, IWebHostEnvironment webHostEnvironment)
        {
            _autorRepository = autorRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("{id}")]
        public async Task<Autor> BuscaAsync([FromRoute] decimal id)
        {
            return await _autorRepository.BuscaAsync(id);
        }

        [HttpGet()]
        public async Task<IEnumerable<Autor>> ListarTodosAsync()
        {
            return await _autorRepository.ListarTodosAsync();
        }

        [HttpPost()]
        public IActionResult Gravar(Autor model)
        {
            var validator = new AutorValidator();
            var result = validator.Validate(model);

            if (!result.IsValid)
                return BadRequest(result.Errors);

            try
            {
                if (model.CodAu != 0)
                    _autorRepository.AtualizarAsync(model);
                else
                    _autorRepository.CriarAsync(model);
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
                _autorRepository.RemoverAsync(id);
            }
            catch (Exception)
            {
                return BadRequest("Ocorreu um erro, verifique com um administrador.");
            }
            return Ok(new { message = "Item removido com sucesso." });
        }
       

    }
}