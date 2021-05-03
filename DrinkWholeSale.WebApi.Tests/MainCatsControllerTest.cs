using DrinkWholeSale.Persistence;
using DrinkWholeSale.Persistence.DTO;
using DrinkWholeSale.Persistence.Services;
using DrinkWholeSale.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DrinkWholeSale.WebApi.Tests
{
    public class MainCatsControllerTest :IDisposable
    {
        private DrinkWholeSaleDbContext _context;
        private DrinkWholeSaleService _service;
        private MainCatsController _controller;

        public MainCatsControllerTest()
        {
            var options = new DbContextOptionsBuilder<DrinkWholeSaleDbContext>()
                .UseInMemoryDatabase("TestDb").Options;

            _context = new DrinkWholeSaleDbContext(options);

            TestDbInitializer.Initialize(_context);
            
            _service = new DrinkWholeSaleService(_context);
            _controller = new MainCatsController(_service);
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

        //[Fact]
        //public void GetInvalidListTest()
        //{
        //    // Arrange
        //    var id = 4;

        //    // Act
        //    var result = _controller.GetMainCat(3);

        //    // Assert
        //    Assert.IsAssignableFrom<NotFoundResult>(result.Result);
        //}

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
    }
}
