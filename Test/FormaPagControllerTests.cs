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
    public class FormaPagControllerTests
    {
        private Mock<IFormaPagRepository> _mockFormaPagRepository;
        private Mock<IWebHostEnvironment> _mockWebHostEnvironment;
        private FormaPagController _controller;

        [SetUp]
        public void Setup()
        {
            _mockFormaPagRepository = new Mock<IFormaPagRepository>();
            _mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            _controller = new FormaPagController(_mockFormaPagRepository.Object, _mockWebHostEnvironment.Object);
        }

        [Test]
        public async Task ListarTodosAsync_ReturnsAllFormaPags()
        {
            // Arrange
            var expectedFormaPags = new List<FormaPag>
            {
                new FormaPag(1, "FormaPag 1"),
                new FormaPag(2, "FormaPag 2")
            };
            _mockFormaPagRepository.Setup(repo => repo.ListarTodosAsync()).ReturnsAsync(expectedFormaPags);

            // Act
            var result = await _controller.ListarTodosAsync();

            // Assert
            Assert.That(result, Is.EqualTo(expectedFormaPags));
        }

        [Test]
        public async Task BuscaAsync_ReturnsFormaPag()
        {
            // Arrange
            var formaPagId = 1;
            var expectedFormaPag = new FormaPag(formaPagId, "FormaPag 1");
            _mockFormaPagRepository.Setup(repo => repo.BuscaAsync(formaPagId)).ReturnsAsync(expectedFormaPag);

            // Act
            var result = await _controller.BuscaAsync(formaPagId);

            // Assert
            Assert.That(result, Is.EqualTo(expectedFormaPag));
        }

        [Test]
        public void Gravar_CriarFormaPag_ReturnsOkResult()
        {
            // Arrange
            var newFormaPag = new FormaPag(0, "Nova FormaPag");

            // Act
            var result = _controller.Gravar(newFormaPag);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void Gravar_AtualizarFormaPag_ReturnsOkResult()
        {
            // Arrange
            var updatedFormaPag = new FormaPag(1, "FormaPag Atualizada");

            // Act
            var result = _controller.Gravar(updatedFormaPag);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void Remover_ReturnsOkResult()
        {
            // Arrange
            var formaPagId = 1;

            // Act
            var result = _controller.Remover(formaPagId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
    }
}