using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NextTech.Products.Application.Commands;
using NextTech.Products.Application.Handlers;
using NextTech.Products.Application.Product.Commands;
using NextTech.Products.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NextTech.Products.Application.Tests
{
    public class ProductCommandHandlerTests : IClassFixture<ServiceFixture>
    {
        Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>();
        readonly IMediator mediator;
        private ServiceProvider serviceProvider;
        public ProductCommandHandlerTests(ServiceFixture fixture)
        {
            serviceProvider = fixture.ServiceProvider;
            mediator = serviceProvider.GetService<IMediator>();
        }

        [Theory]
        [MemberData(nameof(ExceptedAddProduct))]
        public async Task For_CreateProductCommand_Should_AddNewProduct(string title, string description, int stock, bool active)
        {
            var command = CreateProductCommand.Create(title, description, stock, active, null);
            var commandHandler = new ProductCommandHandler(mediator, productRepositoryMock.Object);

            var output = await commandHandler.Handle(command, CancellationToken.None);

            productRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Domain.Product>(), It.IsAny<CancellationToken>()), Times.Once);
            productRepositoryMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Theory]
        [InlineData(new object[] { 0, "Product1 Title", "Product1 Description", 100, true })]
        public async void Should_UpdateProductCommand_Throw_NotFoundException(int id, string title, string description, int stock, bool active)
        {
            productRepositoryMock.Setup(st => st.GetAsync(It.IsAny<int>(), default)).Returns(Task.FromResult<Domain.Product>(null));

            var command = UpdateProductCommand.Create(id, title, description);

            var commandHandler = new ProductCommandHandler(mediator, productRepositoryMock.Object);

            Func<Task> act = async () => await commandHandler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<NotFoundException>();
            productRepositoryMock.Verify(x => x.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Theory]
        [InlineData(new object[] { 0, "Product1 Title", "Product1 Description", 100, true })]
        public async void Should_UpdateProductCommand_IsValid(int id, string title, string description, int stock, bool active)
        {
            var product = Domain.Product.Create(DateTime.Now, title, description, stock, active, null);

            productRepositoryMock.Setup(st => st.GetAsync(It.IsAny<int>(), default)).Returns(Task.FromResult<Domain.Product>(product));

            var command = UpdateProductCommand.Create(id, title, description);

            var commandHandler = new ProductCommandHandler(mediator, productRepositoryMock.Object);

            Func<Task> act = async () => await commandHandler.Handle(command, CancellationToken.None);

            act.Should().NotThrow();
            productRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Domain.Product>(), It.IsAny<CancellationToken>()), Times.Once);
            productRepositoryMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            await Task.CompletedTask;
        }

        [Theory]
        [InlineData(new object[] { 0, "Product1 Title", "Product1 Description", 100, true })]
        public async void Should_EnableProductCommand_Throw_NotFoundException(int id, string title, string description, int stock, bool active)
        {
            productRepositoryMock.Setup(st => st.GetAsync(It.IsAny<int>(), default)).Returns(Task.FromResult<Domain.Product>(null));

            var command = EnableProductCommand.Create(id, "");

            var commandHandler = new ProductCommandHandler(mediator, productRepositoryMock.Object);

            Func<Task> act = async () => await commandHandler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<NotFoundException>();
            productRepositoryMock.Verify(x => x.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Theory]
        [InlineData(new object[] { 0, "Product1 Title", "Product1 Description", 100, false })]
        public async void Should_EnableProductCommand_IsValid(int id, string title, string description, int stock, bool active)
        {
            var product = Domain.Product.Create(DateTime.Now, title, description, stock, active, null);

            productRepositoryMock.Setup(st => st.GetAsync(It.IsAny<int>(), default)).Returns(Task.FromResult<Domain.Product>(product));

            var command = EnableProductCommand.Create(id, "");

            var commandHandler = new ProductCommandHandler(mediator, productRepositoryMock.Object);

            Func<Task> act = async () => await commandHandler.Handle(command, CancellationToken.None);

            act.Should().NotThrow();
            productRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Domain.Product>(), It.IsAny<CancellationToken>()), Times.Once);
            productRepositoryMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            await Task.CompletedTask;
        }
        /**/
        [Theory]
        [InlineData(new object[] { 0, "Product1 Title", "Product1 Description", 100, true })]
        public async void Should_DisableProductCommand_Throw_NotFoundException(int id, string title, string description, int stock, bool active)
        {
            productRepositoryMock.Setup(st => st.GetAsync(It.IsAny<int>(), default)).Returns(Task.FromResult<Domain.Product>(null));

            var command = DisableProductCommand.Create(id, "");

            var commandHandler = new ProductCommandHandler(mediator, productRepositoryMock.Object);

            Func<Task> act = async () => await commandHandler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<NotFoundException>();
            productRepositoryMock.Verify(x => x.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Theory]
        [InlineData(new object[] { 0, "Product1 Title", "Product1 Description", 100, true })]
        public async void Should_DisableProductCommand_IsValid(int id, string title, string description, int stock, bool active)
        {
            var product = Domain.Product.Create(DateTime.Now, title, description, stock, active, null);

            productRepositoryMock.Setup(st => st.GetAsync(It.IsAny<int>(), default)).Returns(Task.FromResult<Domain.Product>(product));

            var command = DisableProductCommand.Create(id, "");

            var commandHandler = new ProductCommandHandler(mediator, productRepositoryMock.Object);

            Func<Task> act = async () => await commandHandler.Handle(command, CancellationToken.None);

            act.Should().NotThrow();
            productRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Domain.Product>(), It.IsAny<CancellationToken>()), Times.Once);
            productRepositoryMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            await Task.CompletedTask;
        }

        public static IEnumerable<object[]> ExceptedAddProduct => new List<object[]>()
        {
            new object[] { "Product1 Title", "Product1 Description", 100, true },
            new object[] { "Product2 Title", "Product2 Description", 200, true },
        };
    }
}