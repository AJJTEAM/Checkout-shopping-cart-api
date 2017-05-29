using AutoMapper;
using CheckoutShopping.Infrastructure.Interfaces;
using CheckoutShopping.Service;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace CheckoutShopping.Tests
{
    [TestFixture]
    public class ShoppingListAppServiceTests
    {
        Mock<IShoppingListRepository> mockShoppingListRepository;
        Mock<ILogger<ShoppingListAppService>> mockLogger;
        Mock<IMapper> mockMapper;
        ShoppingListAppService systemUnderTest;

        [Test]
        public async void AddAsync()
        {
            var shoppingItem = new Service.Models.ShoppingItem
            {
                ShoppingItemId = 1,
               
                Product = new Service.Models.Product
                {
                    Name = "Pepsi",
                    Description = "Classic",
                    Quantity = 2,
                    Price = 1
                }
            };
            mockShoppingListRepository = new Mock<IShoppingListRepository>();
            mockShoppingListRepository.Setup(p => p.AddAsync(new Infrastructure.Models.ShoppingItem
            {
                ShoppingItemId = 1,
               
                Product = new Infrastructure.Models.Product
                {
                    Name = "Pepsi",
                    Description = "Classic",
                    Quantity = 2,
                    Price = 1
                }
            }));
            mockLogger = new Mock<ILogger<ShoppingListAppService>>();
            mockMapper = new Mock<IMapper>();

            systemUnderTest = new ShoppingListAppService(mockShoppingListRepository.Object, mockMapper.Object, mockLogger.Object);
            await systemUnderTest.AddAsync(shoppingItem);
            mockShoppingListRepository.VerifyAll();
        }

        [Test]
        public async void GetByProductNameAsync()
        {
            var shoppingItem = new Service.Models.ShoppingItem
            {
                ShoppingItemId = 1,

                Product = new Service.Models.Product
                {
                    Name = "Pepsi",
                    Description = "Classic",
                    Quantity = 2,
                    Price = 1
                }
            };
            mockShoppingListRepository = new Mock<IShoppingListRepository>();
            mockShoppingListRepository.Setup(p => p.AddAsync(new Infrastructure.Models.ShoppingItem
            {
                ShoppingItemId = 1,

                Product = new Infrastructure.Models.Product
                {
                    Name = "Pepsi",
                    Description = "Classic",
                    Quantity = 2,
                    Price = 1
                }
            }));
            mockLogger = new Mock<ILogger<ShoppingListAppService>>();
            mockMapper = new Mock<IMapper>();

            systemUnderTest = new ShoppingListAppService(mockShoppingListRepository.Object, mockMapper.Object, mockLogger.Object);
            await systemUnderTest.AddAsync(shoppingItem);
            mockShoppingListRepository.Setup(p => p.GetByProductNameAsync(shoppingItem.Product.Name)).ReturnsAsync(new Infrastructure.Models.ShoppingItem
            {
                ShoppingItemId = 1,

                Product = new Infrastructure.Models.Product
                {
                    Name = "Pepsi",
                    Description = "Classic",
                    Quantity = 2,
                    Price = 1
                }
            });

            var result = await systemUnderTest.GetByProductNameAsync(shoppingItem.Product.Name);
            Assert.NotNull(result);
            Assert.AreEqual(result.Product.Name,shoppingItem.Product.Name);
        }

        [Test]
        public async void GetAsync()
        {
            var shoppingItem = new Service.Models.ShoppingItem
            {
                ShoppingItemId = 1,

                Product = new Service.Models.Product
                {
                    Name = "Pepsi",
                    Description = "Classic",
                    Quantity = 2,
                    Price = 1
                }
            };
            mockShoppingListRepository = new Mock<IShoppingListRepository>();
            mockShoppingListRepository.Setup(p => p.AddAsync(new Infrastructure.Models.ShoppingItem
            {
                ShoppingItemId = 1,

                Product = new Infrastructure.Models.Product
                {
                    Name = "Pepsi",
                    Description = "Classic",
                    Quantity = 2,
                    Price = 1
                }
            }));
            mockLogger = new Mock<ILogger<ShoppingListAppService>>();
            mockMapper = new Mock<IMapper>();

            systemUnderTest = new ShoppingListAppService(mockShoppingListRepository.Object, mockMapper.Object, mockLogger.Object);
            await systemUnderTest.AddAsync(shoppingItem);
            mockShoppingListRepository.Setup(p => p.GetAsync()).ReturnsAsync(new[]{ new Infrastructure.Models.ShoppingItem
            {
                ShoppingItemId = 1,

                Product = new Infrastructure.Models.Product
                {
                    Name = "Pepsi",
                    Description = "Classic",
                    Quantity = 2,
                    Price = 1
                }
            } });

            var result = await systemUnderTest.GetAsync();
            Assert.NotNull(result);
            Assert.Greater(result.Count(),0);
        }

        [Test]
        public async void UpdateAsync()
        {
            var shoppingItem = new Service.Models.ShoppingItem
            {
                ShoppingItemId = 1,

                Product = new Service.Models.Product
                {
                    Name = "Pepsi",
                    Description = "Classic",
                    Quantity = 2,
                    Price = 1
                }
            };
            mockShoppingListRepository = new Mock<IShoppingListRepository>();
            mockShoppingListRepository.Setup(p => p.AddAsync(new Infrastructure.Models.ShoppingItem
            {
                ShoppingItemId = 1,

                Product = new Infrastructure.Models.Product
                {
                    Name = "Pepsi",
                    Description = "Classic",
                    Quantity = 2,
                    Price = 1
                }
            }));
            mockLogger = new Mock<ILogger<ShoppingListAppService>>();
            mockMapper = new Mock<IMapper>();

            systemUnderTest = new ShoppingListAppService(mockShoppingListRepository.Object, mockMapper.Object, mockLogger.Object);
            await systemUnderTest.AddAsync(shoppingItem);

            var updateShoppingItemRepo= new Infrastructure.Models.ShoppingItem
            {
                ShoppingItemId = 1,

                Product = new Infrastructure.Models.Product
                {
                    Name = "Pepsi",
                    Description = "Classic",
                    Quantity = 7,
                    Price = 1
                }
            };

            mockShoppingListRepository.Setup(p => p.UpdateAsync(updateShoppingItemRepo)).ReturnsAsync(updateShoppingItemRepo);

            var updateShoppingItem = new Service.Models.ShoppingItem
            {
                ShoppingItemId = 1,

                Product = new Service.Models.Product
                {
                    Name = "Pepsi",
                    Description = "Classic",
                    Quantity = 7,
                    Price = 1
                }
            };
            var result = await systemUnderTest.UpdateAsync(updateShoppingItem);
            Assert.NotNull(result);
            Assert.Equals(result.Product.Quantity, updateShoppingItem.Product.Quantity);
        }
    }
}
