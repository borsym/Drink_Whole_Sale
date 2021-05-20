using DrinkWholeSale.Persistence;
using DrinkWholeSale.Persistence.DTO;
using DrinkWholeSale.Persistence.Services;
using DrinkWholeSale.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Xunit;

namespace DrinkWholeSale.WebApi.Tests
{
    public class MainCatsControllerTest :IDisposable
    {
        private DrinkWholeSaleDbContext _context;
        private DrinkWholeSaleService _service;
        private MainCatsController _controller;
        private SubCatsController _contollerSubCat;
        private ProductsController _contollerProducts;
        

        public MainCatsControllerTest()
        {
            var options = new DbContextOptionsBuilder<DrinkWholeSaleDbContext>()
                .UseInMemoryDatabase("TestDb").Options;

            _context = new DrinkWholeSaleDbContext(options);

            TestDbInitializer.Initialize(_context);
            //var userManager = new UserManager<Guest>(
            //    new UserStore<Guest>(_context), null,
            //    new PasswordHasher<Guest>(), null, null, null, null, null, null);

            //var user = new Guest { UserName = "testName", Id = 1};
            //userManager.CreateAsync(user, "testPassword").Wait();

            _service = new DrinkWholeSaleService(_context);
            _controller = new MainCatsController(_service);
            _contollerSubCat = new SubCatsController(_service);
            _contollerProducts = new ProductsController(_service);
            

            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, "testName"),
                new Claim(ClaimTypes.NameIdentifier, "testId"),
            });
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = claimsPrincipal
                }
            };
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public void GetListsTest()
        {
            // Act
            var result = _controller.GetMainCats();

            // Assert
            var content = Assert.IsAssignableFrom<IEnumerable<MainCatDto>>(result.Value);
            Assert.Equal(2, content.Count());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetListByIdTest(Int32 id)
        {
            // Act
            var result = _controller.GetMainCat(id);

            // Assert
            var content = Assert.IsAssignableFrom<MainCatDto>(result.Value);
            Assert.Equal(id, content.Id);
        }

        [Fact]
        public void GetInvalidListTest()
        {
            // Arrange
            var id = 4;

            // Act
            var result = _controller.GetMainCat(3);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result.Result);
        }

        [Fact]
        public void PostListTest()
        {
            // Arrange
            var newList = new MainCatDto { Name = "New test list" };
            var count = _context.MainCats.Count();

            // Act
            var result = _controller.PostMainCat(newList);

            // Assert
            var objectResult = Assert.IsAssignableFrom<CreatedAtActionResult>(result.Result);
            var content = Assert.IsAssignableFrom<MainCatDto>(objectResult.Value);
            Assert.Equal(count + 1, _context.MainCats.Count());
        }


        [Fact]
        public void GetSubCatsTest()
        {
            // Act
            var result = _contollerSubCat.GetSubCats(1);

            // Assert
            var content = Assert.IsAssignableFrom<IEnumerable<SubCatDto>>(result.Value);
            Assert.Equal(2, content.Count());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetSubCatByIdTest(Int32 id)
        {
            // Act
            var result = _contollerSubCat.GetSubCat(id);

            // Assert
            var content = Assert.IsAssignableFrom<SubCatDto>(result.Value);
            Assert.Equal(id, content.Id);
        }

        [Fact]
        public void GetInvalidSubCatTest()
        {
            // Arrange
            var id = 4;

            // Act
            var result = _contollerSubCat.GetSubCat(30);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result.Result);
        }
        [Fact]
        public void PostSubCatTest()
        {
            // Arrange
            var newList = new SubCatDto { Name = "New test list" };
            var count = _context.SubCats.Count();

            // Act
            var result = _contollerSubCat.PostSubCat(newList);

            // Assert
            var objectResult = Assert.IsAssignableFrom<CreatedAtActionResult>(result.Result);
            var content = Assert.IsAssignableFrom<SubCatDto>(objectResult.Value);
            Assert.Equal(count + 1, _context.SubCats.Count());
        }


        [Fact]
        public void GetProductTest()
        {
            // Act
            var result = _contollerProducts.GetProducts(1);

            // Assert
            var content = Assert.IsAssignableFrom<IEnumerable<ProductDto>>(result.Value);
            Assert.Equal(2, content.Count());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetProductByIdTest(Int32 id)
        {
            // Act
            var result = _contollerProducts.GetProduct(id);

            // Assert
            var content = Assert.IsAssignableFrom<ProductDto>(result.Value);
            Assert.Equal(id, content.Id);
        }

      

        [Fact]
        public void PostProductTest()
        {
            // Arrange
            var newList = new ProductDto { Name = "New test list", Description = "asd", Quantity = 1, NetPrice = 100 };
            var count = _context.Products.Count();

            // Act
            var result = _contollerProducts.PostProduct(newList);

            // Assert
            var objectResult = Assert.IsAssignableFrom<CreatedAtActionResult>(result.Result);
            var content = Assert.IsAssignableFrom<ProductDto>(objectResult.Value);
            Assert.Equal(count + 1, _context.Products.Count());
        }

       

    }
}
