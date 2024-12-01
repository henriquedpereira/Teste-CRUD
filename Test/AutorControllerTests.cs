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
    public class AutorControllerTests
    {
        private Mock<IAutorRepository> _mockAutorRepository;
        private Mock<IWebHostEnvironment> _mockWebHostEnvironment;
        private AutorController _controller;

        [SetUp]
        public void Setup()
        {
            _mockAutorRepository = new Mock<IAutorRepository>();
            _mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            _controller = new AutorController(_mockAutorRepository.Object, _mockWebHostEnvironment.Object);
        }

        [Test]
        public async Task ListarTodosAsync_ReturnsAllAutores()
        {
            // Arrange
            var expectedAutores = new List<Autor>
            {
                new Autor(1, "Autor 1"),
                new Autor(2, "Autor 2")
            };
            _mockAutorRepository.Setup(repo => repo.ListarTodosAsync()).ReturnsAsync(expectedAutores);

            // Act
            var result = await _controller.ListarTodosAsync();

            // Assert
            Assert.That(result, Is.EqualTo(expectedAutores));
        }

        [Test]
        public async Task BuscaAsync_ReturnsAutor()
        {
            // Arrange
            var autorId = 1;
            var expectedAutor = new Autor(autorId, "Autor 1");
            _mockAutorRepository.Setup(repo => repo.BuscaAsync(autorId)).ReturnsAsync(expectedAutor);

            // Act
            var result = await _controller.BuscaAsync(autorId);

            // Assert
            Assert.That(result, Is.EqualTo(expectedAutor));
        }

        [Test]
        public void Gravar_CriarAutor_ReturnsOkResult()
        {
            // Arrange
            var newAutor = new Autor(0, "Novo Autor");

            // Act
            var result = _controller.Gravar(newAutor);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void Gravar_AtualizarAutor_ReturnsOkResult()
        {
            // Arrange
            var updatedAutor = new Autor(1, "Autor Atualizado");

            // Act
            var result = _controller.Gravar(updatedAutor);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void Remover_ReturnsOkResult()
        {
            // Arrange
            var autorId = 1;

            // Act
            var result = _controller.Remover(autorId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
    }
}