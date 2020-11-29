using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using ProductInventory.Domain.Models;
using ProductInventory.Domain.Providers;
using ProductInventory.Domain.Queries.GetSortedProduct;
using ProductInventory.Domain.Services;
using ProductInventory.Domain.Tests.Helpers;

namespace ProductInventory.Domain.Tests.Queries
{
    public class GetSortedProductQueryHandlerTests
    {
        private Mock<IProductService> _mockProductService;
        private Mock<IShopperHistoryService> _mockShopperHistoryService;
        private GetSortedProductQueryHandler _getSortedProductQueryHandler;

        [SetUp]
        public void SetUp()
        {
            _mockProductService = new Mock<IProductService>();
            _mockShopperHistoryService = new Mock<IShopperHistoryService>();
            _getSortedProductQueryHandler = new GetSortedProductQueryHandler(
                _mockProductService.Object,
                new RecommendationProvider(_mockShopperHistoryService.Object)
            );
            
            _mockProductService.Setup(ps => ps.GetProducts())
                .ReturnsAsync(DataGenerator.NotSortedProductsFormLowToHigh); //Shuffled Product List
            _mockShopperHistoryService
                .Setup(sh => sh.GetShopperHistory())
                .ReturnsAsync(new List<ShopperHistory>()); //Empty ShoppingHistory
        }


        [Test]
        public async Task Handle_ShouldReturnEmptyListOfProducts_WhenSortOptionsEqualLowAndProductListIsEmpty()
        {
            // Arrange
            var getSortedProductQuery = new GetSortedProductQuery("Low");
            _mockProductService.Setup(ps => ps.GetProducts())
                .ReturnsAsync(new List<Product>()); //No Products

            // Act
            var getSortedProductQueryResponse = await _getSortedProductQueryHandler.Handle(getSortedProductQuery);

            // Assert
            getSortedProductQueryResponse.Products.Should().BeEmpty();
        }


        [Test]
        public async Task Handle_ShouldReturnSortedFromLowToHigh_WhenSortOptionsEqualLowAndProductListIsNotEmpty()
        {
            // Arrange
            var getSortedProductQuery = new GetSortedProductQuery("Low");

            // Act
            var getSortedProductQueryResponse = await _getSortedProductQueryHandler.Handle(getSortedProductQuery);

            // Assert
            getSortedProductQueryResponse.Products.Should().Equal(DataGenerator.SortedProductsFormLowToHigh);
        }


        [Test]
        public async Task Handle_ShouldReturnSortedFromHighToLow_WhenSortOptionsEqualHighAndProductListIsNotEmpty()
        {
            // Arrange
            var getSortedProductQuery = new GetSortedProductQuery("High");

            // Act
            var getSortedProductQueryResponse = await _getSortedProductQueryHandler.Handle(getSortedProductQuery);

            // Assert
            getSortedProductQueryResponse.Products.Should().Equal(DataGenerator.SortedProductsFormHighToLow);
        }

        [Test]
        public async Task Handle_ShouldReturnSortedAscending_WhenSortOptionsEqualAscendingAndProductListIsNotEmpty()
        {
            // Arrange
            var getSortedProductQuery = new GetSortedProductQuery("Ascending");

            // Act
            var getSortedProductQueryResponse = await _getSortedProductQueryHandler.Handle(getSortedProductQuery);

            // Assert
            getSortedProductQueryResponse.Products.Should().Equal(DataGenerator.SortedProductsAscending);
        }

        [Test]
        public async Task Handle_ShouldReturnSortedDescending_WhenSortOptionsEqualAscendingAndProductListIsNotEmpty()
        {
            // Arrange
            var getSortedProductQuery = new GetSortedProductQuery("Descending");

            // Act
            var getSortedProductQueryResponse = await _getSortedProductQueryHandler.Handle(getSortedProductQuery);

            // Assert
            getSortedProductQueryResponse.Products.Should().Equal(DataGenerator.SortedProductsDescending);
        }
        
        [Test]
        public async Task Handle_ShouldReturnSortedRecommended_WhenSortOptionsEqualRecommendedAndProductListIsNotEmpty()
        {
            // Arrange
            var getSortedProductQuery = new GetSortedProductQuery("Recommended");

            // Act
            var getSortedProductQueryResponse = await _getSortedProductQueryHandler.Handle(getSortedProductQuery);

            // Assert
            getSortedProductQueryResponse.Products.Should().Equal(DataGenerator.SortedProductsBasedOnRecommended);
        }
        
    }
}