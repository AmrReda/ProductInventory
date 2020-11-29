using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductInventory.Domain;
using ProductInventory.Domain.Models;

namespace ProductInventory.Api.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly GetSortedProductQueryHandler _getSortedProductQueryHandler;

        public ProductController(
            ILogger<ProductController> logger,
            GetSortedProductQueryHandler getSortedProductQueryHandler
        )
        {
            _logger = logger;
            _getSortedProductQueryHandler = getSortedProductQueryHandler;
        }

        [HttpGet("sort")]
        [ProducesResponseType(typeof(IEnumerable<Product>), 200)]

        public async Task<ActionResult> Sort([FromQuery] string sortOption)
        {
            var getSortedProductQuery = new GetSortedProductQuery(sortOption);
            var queryResponse = await _getSortedProductQueryHandler.Handle(getSortedProductQuery);
            return Ok(queryResponse.Products);
        }
    }
}