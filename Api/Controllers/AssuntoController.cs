using Core.Entities;
using Core.Entities.Validators;
using Core.Interfaces.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssuntoController : ControllerBase
    {
        private readonly IAssuntoRepository _assuntoRepository;

        public AssuntoController(IAssuntoRepository assuntoRepository)
        {
            _assuntoRepository = assuntoRepository;
        }

        [HttpGet("{id}")]
        public async Task<Assunto> BuscaAsync([FromRoute] decimal id)
        {
            return await _assuntoRepository.Busca(id);
        }

        [HttpGet()]
        public async Task<IEnumerable<Assunto>> ListarTodosAsync()
        {
            return await _assuntoRepository.ListarTodos();
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
                    _assuntoRepository.Atualizar(model);
                else
                    _assuntoRepository.Criar(model);
            }
            catch (Exception)
            {
                return BadRequest("Ocorreu um erro, verifique com um administrador.");
            }

            return Ok();
        }

        [HttpDelete()]
        public IActionResult Remover([FromQuery] decimal id)
        {
            return Ok();
        }


    }
}