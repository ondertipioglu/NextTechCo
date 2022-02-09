using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NextTech.Core.MediatR.Commands;
using NextTech.Core.MediatR.Queries;
using NextTech.Products.Api.Requests.Product;
using NextTech.Products.Application.Commands;
using NextTech.Products.Application.Product.Commands;
using NextTech.Products.Application.Product.Dtos;
using NextTech.Products.Application.Product.Queries;
using System.Threading.Tasks;

namespace NextTech.Products.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ILogger<ProductController> logger;
        private readonly ICommandBus commandBus;
        private readonly IQueryBus queryBus;

        public ProductController(
            IMediator mediator,
             ICommandBus commandBus,
            IQueryBus queryBus,
            ILogger<ProductController> logger
            )
        {
            //TODO : Guard
            //Guard.Against.Null(commandBus, nameof(commandBus));
            //Guard.Against.Null(queryBus, nameof(queryBus));
            this.mediator = mediator;
            this.commandBus = commandBus;
            this.queryBus = queryBus;
            this.logger = logger;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            //var product = await mediator.Send(GetProductById.Create(id)); ;
            var product = await queryBus.Send<GetProductByIdQuery, ProductDto>(GetProductByIdQuery.Create(id));
            return Ok(product);
        }

        [HttpGet("search-products")]
        public async Task<IActionResult> SearchProducts([FromQuery] SearchProductRequest query)
        {
            var searchProductQuery = SearchProductQuery.Create(query.Filter, query.MinimumStock, query.MaximumStock, query.PageNumber, query.PageSize);

            var result = await queryBus.Send<SearchProductQuery, SearchProductQueryResponse>(searchProductQuery);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateProductRequest request)
        {
            var command = CreateProductCommand.Create(
                request.Title,
                request.Description,
                request.Stock,
                request.Active,
                request.Category
                );

            var response = await mediator.Send(command);

            return Created("v1/Product", response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProductRequest request)
        {
            var command = UpdateProductCommand.Create(
                request.ProductId,
                request.Title,
                request.Description
                );
            //await commandBus.Send<UpdateProductCommand>(command);
            await mediator.Send(command);
            return NoContent();
        }

        [HttpPatch("Active")]
        public async Task<IActionResult> Active([FromBody] EnableProductRequest request)
        {
            var command = EnableProductCommand.Create(request.ProductId, request.Comment);
            await commandBus.Send<EnableProductCommand>(command);

            return Ok();
        }

        [HttpPatch("Passive")]
        public async Task<IActionResult> Passive([FromBody] DisableProductRequest request)
        {
            var command = DisableProductCommand.Create(request.ProductId, request.Comment);
            await commandBus.Send<DisableProductCommand>(command);

            return Ok();
        }
    }
}