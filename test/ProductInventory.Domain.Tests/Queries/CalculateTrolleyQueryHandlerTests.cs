using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using ProductInventory.Domain.Models;
using ProductInventory.Domain.Queries.CalculateTrolley;

namespace ProductInventory.Domain.Tests.Queries
{
    public class CalculateTrolleyQueryHandlerTests
    {
        
        private readonly Product _productA = new Product("Product A", 14.5);
        private readonly Product _productB = new Product("Product B", 29.6);
        private readonly Product _productC = new Product("Product C", 24);

        
        [Test]
        public void Handle_ShouldApplySpecialForOneProduct()
        {
            // Arrange
            var calculateTrolleyQuery = new CalculateTrolleyQuery
            {
                Products = new List<Product>
                {
                    _productA,
                    _productB
                },
                Specials = new List<Special>
                {
                    new Special(new List<Quantity>
                    {
                        new Quantity("Product A", 3)
                    }, 20)
                },
                Quantities = new List<Quantity>
                {
                    new Quantity("Product A", 3)
                }
            };

            // Act
            var total = new CalculateTrolleyQueryHandler().Handle(calculateTrolleyQuery);

            // Assert
            total.Should().Be(20.0d);
        }

        [Test]
        public void Handle_ShouldSpecialsAppliedForMultipleProducts()
        {
            // Arrange
            var calculateTrolleyQuery = new CalculateTrolleyQuery
            {
                Products = new List<Product>
                {
                    _productA,
                    _productB,
                    _productC
                },
                Specials = new List<Special>
                {
                    new Special(new List<Quantity>
                    {
                        new Quantity("Product A", 3),
                        new Quantity("Product B", 3)
                    }, 20)
                },
                Quantities = new List<Quantity>
                {
                    new Quantity("Product A", 4),
                    new Quantity("Product B", 4)
                }
            };

            // Act
            var total = new CalculateTrolleyQueryHandler().Handle(calculateTrolleyQuery);

            // Assert
            total.Should().Be(64.1d);
        }

        [Test]
        public void Handle_SpecialsAppliedForMultipleProductsAndProductPricesAppliedCorrectlyForItemsMoreThanOne()
        {
            // Arrange
            var calculateTrolleyQuery = new CalculateTrolleyQuery
            {
                Products = new List<Product>
                {
                    _productA,
                    _productB,
                    _productC
                },
                Specials = new List<Special>
                {
                    new Special(new List<Quantity>
                    {
                        new Quantity("Product A", 3),
                        new Quantity("Product B", 3)
                    }, 20)
                },
                Quantities = new List<Quantity>
                {
                    new Quantity("Product A", 5),
                    new Quantity("Product B", 5)
                }
            };

            // Act
            var total = new CalculateTrolleyQueryHandler().Handle(calculateTrolleyQuery);

            // Assert
            total.Should().Be(108.2);
        }

        [Test]
        public void Handle_SpecialsAppliedMultipleTimes()
        {
            // Arrange
            var calculateTrolleyQuery = new CalculateTrolleyQuery
            {
                Products = new List<Product>
                {
                    _productA,
                    _productB,
                    _productC
                },
                Specials = new List<Special>
                {
                    new Special(new List<Quantity>
                    {
                        new Quantity("Product A", 3),
                        new Quantity("Product B", 3)
                    }, 20)
                },
                Quantities = new List<Quantity>
                {
                    new Quantity("Product A", 6),
                    new Quantity("Product B", 6)
                }
            };

            // Act
            var total = new CalculateTrolleyQueryHandler().Handle(calculateTrolleyQuery);

            // Assert
            total.Should().Be(40.0d);
        }

        [Test]
        public void Handle_SpecialsDoesNotApply()
        {
            // Arrange
            var calculateTrolleyQuery = new CalculateTrolleyQuery
            {
                Products = new List<Product>
                {
                    _productA,
                    _productB,
                    _productC
                },
                Specials = new List<Special>
                {
                    new Special(new List<Quantity>
                    {
                        new Quantity("Product A", 3),
                        new Quantity("Product B", 3)
                    }, 20)
                },
                Quantities = new List<Quantity>
                {
                    new Quantity("Product C", 1),
                }
            };

            // Act
            var total = new CalculateTrolleyQueryHandler().Handle(calculateTrolleyQuery);

            // Assert
            total.Should().Be(24d);
        }

        [Test]
        public void Handle_InValidPurchasedQuantities()
        {
            // Arrange
            var calculateTrolleyQuery = new CalculateTrolleyQuery
            {
                Products = new List<Product>
                {
                    _productA,
                    _productB,
                    _productC
                },
                Specials = new List<Special>
                {
                    new Special(new List<Quantity>
                    {
                        new Quantity("Product A", 3),
                        new Quantity("Product B", 3)
                    }, 20)
                },
                Quantities = new List<Quantity>
                {
                    new Quantity("Product C", 1),
                    new Quantity("Product C", 1),
                }
            };

            // Act/Assert
            Assert.Throws<ArgumentException>(() => new CalculateTrolleyQueryHandler().Handle(calculateTrolleyQuery));
        }
    }
}