using Api.Controllers;
using Core.Entities;
using Core.Interfaces.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace Test
{
    public class LivroControllerTests
    {
        private Mock<ILivroRepository> _mockLivroRepository;
        private Mock<IWebHostEnvironment> _mockWebHostEnvironment;
        private Mock<IAssuntoRepository> _mockAssuntoRepository;
        private Mock<IAutorRepository> _mockAutorRepository;
        private Mock<IFormaPagRepository> _mockFormaPagRepository;
        private LivroController _controller;

        [SetUp]
        public void Setup()
        {
            _mockLivroRepository = new Mock<ILivroRepository>();
            _mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            _mockAssuntoRepository = new Mock<IAssuntoRepository>();
            _mockAutorRepository = new Mock<IAutorRepository>();
            _mockFormaPagRepository = new Mock<IFormaPagRepository>();
            _controller = new LivroController(
                _mockLivroRepository.Object,
                _mockWebHostEnvironment.Object,
                _mockAssuntoRepository.Object,
                _mockAutorRepository.Object,
                _mockFormaPagRepository.Object
            );
        }

        [Test]
        public async Task ListarTodosAsync_ReturnsAllLivros()
        {
            // Arrange
            var expectedLivros = new List<Livro>
            {
                new Livro(1, "Livro 1", "Editora 1", 1, "2023"),
                new Livro(2, "Livro 2", "Editora 2", 2, "2022")
            };
            _mockLivroRepository.Setup(repo => repo.ListarTodosAsync()).ReturnsAsync(expectedLivros);

            // Act
            var result = await _controller.ListarTodosAsync();

            // Assert
            Assert.That(result, Is.EqualTo(expectedLivros));
        }

        [Test]
        public async Task BuscaAsync_ReturnsLivro()
        {
            // Arrange
            var livroId = 1;
            var expectedLivro = new Livro(livroId, "Livro 1", "Editora 1", 1, "2023");
            _mockLivroRepository.Setup(repo => repo.BuscaAsync(livroId)).ReturnsAsync(expectedLivro);

            // Act
            var result = await _controller.BuscaAsync(livroId);

            // Assert
            Assert.That(result, Is.EqualTo(expectedLivro));
        } 

        [Test]
        public async Task Gravar_AtualizarLivro_ReturnsOkResult()
        {
            // Arrange
            var updatedLivro = new Livro(1, "Livro Atualizado", "Editora Atualizada", 2, "2023")
            {
                ListaAssuntos = new List<SelectListItem>
                {
                    new SelectListItem { text = "Assunto 1", value = "1", selected = true },
                    new SelectListItem { text = "Assunto 2", value = "2", selected = false }
                },
                ListaAutores = new List<SelectListItem>
                {
                    new SelectListItem { text = "Autor 1", value = "1", selected = true },
                    new SelectListItem { text = "Autor 2", value = "2", selected = false }
                },
                ListaFormasPag = new List<SelectListItem>
                {
                    new SelectListItem { text = "FormaPag 1", value = "1", value2 = 100, selected = true },
                    new SelectListItem { text = "FormaPag 2", value = "2", value2 = 200, selected = false }
                }
            };

            // Act
            var result = await _controller.Gravar(updatedLivro);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task Remover_ReturnsOkResult()
        {
            // Arrange
            var livroId = 1;

            // Act
            var result = await _controller.Remover(livroId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
    }
}