using MediatR;
using NextTech.Core.MediatR.Commands;
using NextTech.Products.Application.Commands;
using NextTech.Products.Application.Product.Commands;
using NextTech.Products.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NextTech.Products.Application.Handlers
{
    public class ProductCommandHandler :
        ICommandHandler<CreateProductCommand, int>,
        ICommandHandler<UpdateProductCommand>,
        ICommandHandler<EnableProductCommand>,
        ICommandHandler<DisableProductCommand>

    {
        private readonly IMediator mediator;
        private readonly IProductRepository productRepository;

        public ProductCommandHandler(IMediator mediator, IProductRepository productRepository)
        {
            this.mediator = mediator;
            this.productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }



        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            /** If the product id comes from the client, validate product id for AlreadyExistsException **/

            var product = Domain.Product.Create(DateTime.Now, request.Title, request.Description, request.Stock, request.Active, request.CategoryId);

            await productRepository.AddAsync(product, cancellationToken);
            await productRepository.SaveChangesAsync(cancellationToken);

            return product.Id;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetAsync(request.Id, cancellationToken);

            if (product is null) throw NotFoundException.For<Domain.Product>(request.Id); 

            product.ChangeTitleAndDescriptionInfo(request.Title, request.Description);

            await productRepository.UpdateAsync(product, cancellationToken);
            await productRepository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        public async Task<Unit> Handle(EnableProductCommand request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetAsync(request.Id, cancellationToken) 
                                ?? throw NotFoundException.For<Domain.Product>(request.Id); ;

            product.Enable();

            await productRepository.UpdateAsync(product, cancellationToken);
            await productRepository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        public async Task<Unit> Handle(DisableProductCommand request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetAsync(request.Id, cancellationToken)
                                ?? throw NotFoundException.For<Domain.Product>(request.Id); ;

            product.Disable();

            await productRepository.UpdateAsync(product, cancellationToken);
            await productRepository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
