using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ProductInventory.Api.Controllers;
using ProductInventory.Domain.Models;
using ProductInventory.Domain.Providers;
using ProductInventory.Domain.Queries.GetSortedProduct;
using ProductInventory.Domain.Services;

namespace ProductInventory.Api.Tests
{
    public class ProductSortControllerTests
    {
        private Mock<ILogger<ProductController>> _mockLogger;
        private Mock<IProductService> _mockProductService;
        private Mock<IShopperHistoryService> _mockShopperHistoryService;
        
        
        [Test]
        public async Task Should_GetSortProduct_ReturnsCorrectDetails()
        {
            //Arrange
            _mockLogger = new Mock<ILogger<ProductController>>();
            _mockProductService = new Mock<IProductService>();
            _mockShopperHistoryService = new Mock<IShopperHistoryService>();
            _mockProductService.Setup(ps => ps.GetProducts())
                .ReturnsAsync(new List<Product>()); //Shuffled Product List
            _mockShopperHistoryService
                .Setup(sh => sh.GetShopperHistory())
                .ReturnsAsync(new List<ShopperHistory>()); //Empty ShoppingHistory
            // Act
            var result = await new ProductController(_mockLogger.Object,
                    new GetSortedProductQueryHandler(_mockProductService.Object,
                        new RecommendationProvider(_mockShopperHistoryService.Object)))
                .Sort("Low");
            
            // Assert
            var actionResult = (ActionResult<IEnumerable<Product>>)(result);
            Assert.IsNotNull(actionResult);
            var okObjectResult = (OkObjectResult)actionResult.Result;
            Assert.IsNotNull(okObjectResult);
            okObjectResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            ((IEnumerable<Product>) okObjectResult.Value).Should().BeEmpty();
        }
    }
}