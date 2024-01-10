using Microsoft.EntityFrameworkCore;
using Stock.Domain.Entities;
using Stock.Infrastructure.Persistence;
using Stock.Infrastructure.Repositories;

namespace Stock.UnitTests
{
    [TestClass]
    public class ProductRepositoryTests
    {
        private ProductRepository _productRepository;
        private ProductContext _context;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ProductContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new ProductContext(options);
            _productRepository = new ProductRepository(_context);
        }

        [TestMethod]
        public async Task CalculateBundlesFromStock_ShouldReturnCorrectBundles()
        {
            // Arrange
            var wheel = new Product { Id = 1, Name = "Wheel", StockQuantity = 35 };
            var pedal = new Product { Id = 2, Name = "Pedal", StockQuantity = 60 };
            var seat = new Product { Id = 3, Name = "Seat", StockQuantity = 50 };
            var bike = new Product
            {
                Id = 4,
                Name = "Bike",
                ChildrenProducts = new List<ProductAssociation>
            {
                new ProductAssociation { ChildProductId = 1, RequiredQuantity = 2, ChildProduct = wheel },
                new ProductAssociation { ChildProductId = 2, RequiredQuantity = 2, ChildProduct = pedal },
                new ProductAssociation { ChildProductId = 3, RequiredQuantity = 1, ChildProduct = seat }
            }
            };
            _context.Products.AddRange(new List<Product> { wheel, pedal, seat, bike });
            _context.SaveChanges();

            // Act
            var result = await _productRepository.CalculateBundlesFromStock(4);

            // Assert
            Assert.AreEqual(17, result);
        }

        [TestMethod]
        public async Task CalculateBundlesFromStock_ShouldReturnCorrectBundles2()
        {
            int multiplier = 2;
            // Arrange
            var wheel = new Product { Id = 1, Name = "Wheel", StockQuantity = 2 * multiplier };
            var pedal = new Product { Id = 2, Name = "Pedal", StockQuantity = 2 * multiplier };
            var seat = new Product { Id = 3, Name = "Seat", StockQuantity = 1 * multiplier };
            var bike = new Product
            {
                Id = 4,
                Name = "Bike",
                ChildrenProducts = new List<ProductAssociation>
            {
                new ProductAssociation { ChildProductId = 1, RequiredQuantity = 2, ChildProduct = wheel },
                new ProductAssociation { ChildProductId = 2, RequiredQuantity = 2, ChildProduct = pedal },
                new ProductAssociation { ChildProductId = 3, RequiredQuantity = 1, ChildProduct = seat }
            }
            };
            _context.Products.AddRange(new List<Product> { wheel, pedal, seat, bike });
            _context.SaveChanges();

            // Act
            var result = await _productRepository.CalculateBundlesFromStock(4);

            // Assert
            Assert.AreEqual(multiplier, result);
        }

        [TestMethod]
        public async Task CalculateBundlesFromStock_ShouldReturnCorrectBundles3()
        {
            int multiplier = 2;
            // Arrange
            var wheel = new Product { Id = 1, Name = "Wheel", StockQuantity = 2 * multiplier + 2 };
            var pedal = new Product { Id = 2, Name = "Pedal", StockQuantity = 2 * multiplier };
            var seat = new Product { Id = 3, Name = "Seat", StockQuantity = 1 * multiplier };
            var bike = new Product
            {
                Id = 4,
                Name = "Bike",
                ChildrenProducts = new List<ProductAssociation>
            {
                new ProductAssociation { ChildProductId = 1, RequiredQuantity = 2, ChildProduct = wheel },
                new ProductAssociation { ChildProductId = 2, RequiredQuantity = 2, ChildProduct = pedal },
                new ProductAssociation { ChildProductId = 3, RequiredQuantity = 1, ChildProduct = seat }
            }
            };
            _context.Products.AddRange(new List<Product> { wheel, pedal, seat, bike });
            _context.SaveChanges();

            // Act
            var result = await _productRepository.CalculateBundlesFromStock(4);

            // Assert
            Assert.AreEqual(multiplier, result);
        }

        [TestMethod]
        public async Task CalculateBundlesFromStock_ShouldReturnCorrectBundles4()
        {
            // Arrange
            var frame = new Product { Id = 1, Name = "Frame", StockQuantity = 5 };
            var tube = new Product { Id = 2, Name = "Tube", StockQuantity = 6 };
            var wheel = new Product
            {
                Id = 3,
                Name = "Wheel",
                StockQuantity = 6,
                ChildrenProducts = new List<ProductAssociation>
            {
                new ProductAssociation { ChildProductId = 1, RequiredQuantity = 1, ChildProduct = frame },
                new ProductAssociation { ChildProductId = 2, RequiredQuantity = 1, ChildProduct = tube }
            }
            };
            var pedal = new Product { Id = 4, Name = "Pedal", StockQuantity = 6 };
            var seat = new Product { Id = 5, Name = "Seat", StockQuantity = 3 };
            var bike = new Product
            {
                Id = 6,
                Name = "Bike",
                ChildrenProducts = new List<ProductAssociation>
            {
                new ProductAssociation { ChildProductId = 3, RequiredQuantity = 2, ChildProduct = wheel },
                new ProductAssociation { ChildProductId = 4, RequiredQuantity = 2, ChildProduct = pedal },
                new ProductAssociation { ChildProductId = 5, RequiredQuantity = 1, ChildProduct = seat }
            }
            };
            _context.Products.AddRange(new List<Product> { frame, tube, wheel, pedal, seat, bike });
            _context.SaveChanges();

            // Act
            var result = await _productRepository.CalculateBundlesFromStock(6);

            // Assert
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public async Task CalculateBundlesFromStock_ShouldReturnCorrectBundles5()
        {
            // Arrange
            var frame = new Product { Id = 1, Name = "Frame", StockQuantity = 60 };
            var tube = new Product { Id = 2, Name = "Tube", StockQuantity = 35 };
            var wheel = new Product
            {
                Id = 3,
                Name = "Wheel",
                StockQuantity = 60,
                ChildrenProducts = new List<ProductAssociation>
            {
                new ProductAssociation { ChildProductId = 1, RequiredQuantity = 1, ChildProduct = frame },
                new ProductAssociation { ChildProductId = 2, RequiredQuantity = 1, ChildProduct = tube }
            }
            };
            var pedal = new Product { Id = 4, Name = "Pedal", StockQuantity = 60 };
            var seat = new Product { Id = 5, Name = "Seat", StockQuantity = 50 };
            var bike = new Product
            {
                Id = 6,
                Name = "Bike",
                ChildrenProducts = new List<ProductAssociation>
            {
                new ProductAssociation { ChildProductId = 3, RequiredQuantity = 2, ChildProduct = wheel },
                new ProductAssociation { ChildProductId = 4, RequiredQuantity = 2, ChildProduct = pedal },
                new ProductAssociation { ChildProductId = 5, RequiredQuantity = 1, ChildProduct = seat }
            }
            };
            _context.Products.AddRange(new List<Product> { frame, tube, wheel, pedal, seat, bike });
            _context.SaveChanges();

            // Act
            var result = await _productRepository.CalculateBundlesFromStock(6);

            // Assert
            Assert.AreEqual(17, result);
        }
    }
}