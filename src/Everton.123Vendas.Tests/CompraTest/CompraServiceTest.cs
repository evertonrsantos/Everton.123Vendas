using Bogus;
using Everton._123Vendas.Domain.Entities;
using Everton._123Vendas.Domain.Entities.EventMessage;
using Everton._123Vendas.Domain.Interfaces.Events;
using Everton._123Vendas.Domain.Interfaces.Repositories;
using Everton._123Vendas.Domain.Interfaces.Services;
using Everton._123Vendas.Domain.Services;
using NSubstitute;

namespace Everton._123Vendas.Tests.CompraTest
{
    public class CompraServiceTest
    {
        private readonly ICompraRepository _repositoryMock;
        private readonly IItemCompraRepository _itemRepositoryMock;
        private readonly ICompraCriadaPublisher _compraCriadaPublisherMock;
        private readonly ICompraAlteradaPublisher _compraAlteradaPublisherMock;
        private readonly ICompraCanceladaPublisher _compraCanceladaPublisherMock;
        private readonly ICompraService _compraService;
        private readonly Faker _faker;

        public CompraServiceTest()
        {
            DependencyInjection.DependencyInjectionConfig.AddDependencies();

            _repositoryMock = Substitute.For<ICompraRepository>();
            _itemRepositoryMock = Substitute.For<IItemCompraRepository>();
            _compraCriadaPublisherMock = Substitute.For<ICompraCriadaPublisher>();
            _compraAlteradaPublisherMock = Substitute.For<ICompraAlteradaPublisher>();
            _compraCanceladaPublisherMock = Substitute.For<ICompraCanceladaPublisher>();
            _compraService = new CompraService(_repositoryMock, _itemRepositoryMock, _compraCriadaPublisherMock, _compraAlteradaPublisherMock, _compraCanceladaPublisherMock);
            _faker = new Faker();
        }

        #region Criação da Compra

        [Fact]
        public async Task CriarCompraAsync_DeveSalvarCompraEPublicarEvento()
        {
            // Arrange
            var compra = new Compra("9823472vw4g", DateTime.Now, "customer_23ribwviwg4", "LJSP009");
            compra.AlterarItens(
            [
                new ItemCompra("COD0101", 5, 25.34m),
                new ItemCompra("COD0103", 2, 10.14m),
                new ItemCompra("COD0106", 6, 55.89m),
                new ItemCompra("COD0123", 1, 1.34m),
                new ItemCompra("COD0137", 2, 14.10m),
                new ItemCompra("COD0246", 15, 05.34m)
            ]);

            // Act
            await _compraService.CreateAsync(compra);

            // Assert
            await _repositoryMock.Received(1).CreateAsync(compra);
            await _compraCriadaPublisherMock.Received(1).PublishAsync(Arg.Is<CompraCriadaMessage>(message => message.CompraId == compra.Id));
        }

        [Fact]
        public async Task CriarCompraAsync_DeveRetornarErroENaoSalvarNemEnviarEvento()
        {
            // Arrange
            var compra = new Compra("9823472vw4g", DateTime.Now, "customer_23ribwviwg4", "LJSP009");
            compra.AlterarItens(
            [
                new ItemCompra("COD0246", 25, 05.34m)
            ]);

            // Act
            await _compraService.CreateAsync(compra);

            // Assert
            await _repositoryMock.Received(0).CreateAsync(compra);
            await _compraCriadaPublisherMock.Received(0).PublishAsync(Arg.Is<CompraCriadaMessage>(message => message.CompraId == compra.Id));
        }

        #endregion

        #region Alteração da Compra

        [Fact]
        public async Task AlterarCompraAsync_DeveSalvarCompraEPublicarEvento()
        {
            // Arrange
            var compraExistente = new Compra("9823472vw4g", DateTime.Now, "customer_23ribwviwg4", "LJSP009");
            compraExistente.AlterarItens(
            [
                new ItemCompra("COD0101", 5, 25.34m),
                new ItemCompra("COD0103", 2, 10.14m),
                new ItemCompra("COD0106", 6, 55.89m),
                new ItemCompra("COD0123", 1, 1.34m),
                new ItemCompra("COD0137", 2, 14.10m),
                new ItemCompra("COD0246", 15, 05.34m)
            ]);

            _repositoryMock.GetByIdAsync(compraExistente.Id).Returns(compraExistente);

            var compraAlterada = new Compra("9823472vw4g", DateTime.Now, "customer_23ribwviwg4", "LJSP009");
            compraAlterada.AlterarItens(
            [
                new ItemCompra("COD0101", 5, 25.34m),
                new ItemCompra("COD0103", 2, 10.14m),
                new ItemCompra("COD1010", 6, 55.89m),
                new ItemCompra("COD0188", 2, 14.10m),
            ]);

            // Act
            await _compraService.UpdateAsync(compraExistente.Id, compraAlterada);

            // Assert
            await _repositoryMock.Received(1).UpdateAsync(compraExistente);
            await _itemRepositoryMock.Received(1).RemoveRangeAsync(Arg.Any<IEnumerable<ItemCompra>>());
            await _itemRepositoryMock.Received(1).AddRangeAsync(Arg.Any<IEnumerable<ItemCompra>>());
            await _compraAlteradaPublisherMock.Received(1).PublishAsync(Arg.Is<CompraAlteradaMessage>(message => message.CompraId == compraExistente.Id));
        }

        #endregion

        #region Cancelamento da Compra

        [Fact]
        public async Task CancelarCompraAsync_DeveSalvarCompraCancelada()
        {
            //Arrange
            var compra = new Compra("9823472vw4g", DateTime.Now, "customer_23ribwviwg4", "LJSP009");
            _repositoryMock.GetByIdAsync(compra.Id).Returns(compra);

            // Act
            await _compraService.DeleteAsync(compra.Id);

            // Assert
            await _repositoryMock.Received(1).UpdateAsync(compra);
            await _compraCanceladaPublisherMock.Received(1).PublishAsync(Arg.Is<CompraCanceladaMessage>(message => message.CompraId == compra.Id));
        }

        #endregion
    }
}
