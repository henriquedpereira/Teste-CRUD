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
    public class FormaPagController : ControllerBase
    {
        private readonly IFormaPagRepository _formaPagRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FormaPagController(IFormaPagRepository formaPagRepository, IWebHostEnvironment webHostEnvironment)
        {
            _formaPagRepository = formaPagRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("{id}")]
        public async Task<FormaPag> BuscaAsync([FromRoute] decimal id)
        {
            return await _formaPagRepository.BuscaAsync(id);
        }

        [HttpGet()]
        public async Task<IEnumerable<FormaPag>> ListarTodosAsync()
        {
            return await _formaPagRepository.ListarTodosAsync();
        }

        [HttpPost()]
        public IActionResult Gravar(FormaPag model)
        {
            var validator = new FormaPagValidator();
            var result = validator.Validate(model);

            if (!result.IsValid)
                return BadRequest(result.Errors);

            try
            {
                if (model.CodForm != 0)
                    _formaPagRepository.AtualizarAsync(model);
                else
                    _formaPagRepository.CriarAsync(model);
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
                _formaPagRepository.RemoverAsync(id);
            }
            catch (Exception)
            {
                return BadRequest("Ocorreu um erro, verifique com um administrador.");
            }
            return Ok(new { message = "Item removido com sucesso." });
        }
       

    }
}