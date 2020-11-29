using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ProductInventory.Api.Controllers;
using ProductInventory.Api.ViewModels;

namespace ProductInventory.Api.Tests
{
    public class UserControllerTests
    {
        [Test]
        public void Should_UserReturnsCorrectDetails_WithNameEqualAmrKamel()
        {
            // Act
            var result = (OkObjectResult)new UserController().FindUser();

            // Assert
            Assert.IsNotNull(result);
            
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            var userResponse = (UserResponse)result.Value;
            Assert.IsNotNull(userResponse);
            userResponse.Name.Should().Be("Amr Kamel");
            userResponse.Token.Should().Be("108337a6-dbcd-4231-92d4-d4962fc43b71");
        }
    }
}