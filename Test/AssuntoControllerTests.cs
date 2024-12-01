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
    public class AssuntoControllerTests
    {
        private Mock<IAssuntoRepository> _mockAssuntoRepository;
        private Mock<IWebHostEnvironment> _mockWebHostEnvironment;
        private AssuntoController _controller;

        [SetUp]
        public void Setup()
        {
            _mockAssuntoRepository = new Mock<IAssuntoRepository>();
            _mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            _controller = new AssuntoController(_mockAssuntoRepository.Object, _mockWebHostEnvironment.Object);
        }

        [Test]
        public async Task ListarTodosAsync_ReturnsAllAssuntos()
        {
            // Arrange
            var expectedAssuntos = new List<Assunto>
            {
                new Assunto(1, "Assunto 1"),
                new Assunto(2, "Assunto 2")
            };
            _mockAssuntoRepository.Setup(repo => repo.ListarTodosAsync()).ReturnsAsync(expectedAssuntos);

            // Act
            var result = await _controller.ListarTodosAsync();

            // Assert
            Assert.That(result, Is.EqualTo(expectedAssuntos));
        }

        [Test]
        public async Task BuscaAsync_ReturnsAssunto()
        {
            // Arrange
            var assuntoId = 1;
            var expectedAssunto = new Assunto(assuntoId, "Assunto 1");
            _mockAssuntoRepository.Setup(repo => repo.BuscaAsync(assuntoId)).ReturnsAsync(expectedAssunto);

            // Act
            var result = await _controller.BuscaAsync(assuntoId);

            // Assert
            Assert.That(result, Is.EqualTo(expectedAssunto));
        }

        [Test]
        public void Criar_ReturnsOkResult()
        {
            // Arrange
            var newAssunto = new Assunto(3, "Assunto 3");

            // Act
            var result = _controller.Gravar(newAssunto);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void Atualizar_ReturnsOkResult()
        {
            // Arrange
            var updatedAssunto = new Assunto(1, "Assunto Atualizado");

            // Act
            var result = _controller.Gravar(updatedAssunto);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void Remover_ReturnsOkResult()
        {
            // Arrange
            var assuntoId = 1;

            // Act
            var result = _controller.Remover(assuntoId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
    }
}