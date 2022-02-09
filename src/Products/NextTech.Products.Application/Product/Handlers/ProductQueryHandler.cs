using AutoMapper;
using MediatR;
using NextTech.Core.Exceptions;
using NextTech.Core.MediatR.Queries;
using NextTech.Products.Application.Product.Dtos;
using NextTech.Products.Application.Product.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NextTech.Products.Application.Product.Handlers
{
    public class ProductQueryHandler :
        IQueryHandler<GetProductByIdQuery, ProductDto>,
        IQueryHandler<SearchProductQuery, SearchProductQueryResponse>
    {
        private readonly IMediator mediator;
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        public ProductQueryHandler(IMediator mediator, IProductRepository productRepository, IMapper mapper)
        {
            this.mediator = mediator;
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            //Check request
            var product = await productRepository.GetAsync(query.Id, cancellationToken);

            if (product is null) throw AggregateNotFoundException.For<Domain.Product>(query.Id);

            var result = mapper.Map<ProductDto>(product);

            return result;
        }

        public async Task<SearchProductQueryResponse> Handle(SearchProductQuery query, CancellationToken cancellationToken)
        {
            var totalOfProducts = await productRepository.SearchProductsCountAsync(query.Filter, query.MinimumStockFilter, query.MaximumStockFilter, cancellationToken);

            var products = await productRepository.SearchProductsAsync(query.Filter, query.MinimumStockFilter, query.MaximumStockFilter, query.PageNumber, query.PageSize, cancellationToken);

            var result = new SearchProductQueryResponse()
            {
                Data = mapper.Map<IEnumerable<ProductDto>>(products),
                TotalCount = totalOfProducts
            };

            return result;
        }
    }
}
