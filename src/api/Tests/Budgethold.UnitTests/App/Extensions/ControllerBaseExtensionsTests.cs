using System;
using Budgethold.API.Extensions;
using Budgethold.Domain.Common;
using Budgethold.Domain.Common.Errors;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Budgethold.UnitTests.App.Extensions
{
    public class ControllerBaseExtensionsTests
    {
        #region GetResponseFromResult
        [Fact]
        public void GetResponseFromResult_EmptyResult_NoContent()
        {
            // Arrange
            var controllerBase = new TestController();
            var result = new Result();

            // Act
            var response = controllerBase.GetResponseFromResult(result);

            // Assert
            Assert.IsType<NoContentResult>(response);
        }

        [Fact]
        public void GetResponseFromResult_NotEmptySuccesResult_Ok()
        {
            // Arrange
            var controllerBase = new TestController();
            var testDto = new TestDto { Id = 1, Name = "Stefan" };
            var result = new Result<TestDto>(testDto);

            // Act
            var response = controllerBase.GetResponseFromResult(result);

            // Assert
            Assert.IsType<OkObjectResult>(response);
            var responseValue = ((OkObjectResult)response).Value;
            Assert.NotNull(responseValue);
            Assert.IsType<TestDto>(responseValue);
            var returnedDto = (TestDto)responseValue!;
            Assert.Equal(testDto.Id, returnedDto.Id);
            Assert.Equal(testDto.Name, returnedDto.Name);
        }

        [Fact]
        public void GetResponseFromResult_NotFoundError_BadRequest()
        {
            // Arrange
            var controllerBase = new TestController();
            var errorMessage = "Nie znaleziono";
            var error = new NotFoundError(errorMessage);
            var result = new Result(error);

            // Act
            var response = controllerBase.GetResponseFromResult(result);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);
            var responseValue = ((BadRequestObjectResult)response).Value;
            Assert.Equal(errorMessage, responseValue);
        }

        [Fact]
        public void GetResponseFromResult_UnhandledError_UnhandledErrorResultException()
        {
            // Arrange
            var controllerBase = new TestController();
            var error = new TestError("new type of error");
            var result = new Result(error);

            // Act => Assert
            Assert.Throws<NotSupportedException>(() => controllerBase.GetResponseFromResult(result));
        }
        #endregion

        #region Private helpers
        public class TestController : ControllerBase { }

        public record TestError : Error
        {
            public TestError(string original) : base(original) { }
        }

        public record TestDto
        {
            public TestDto()
            {
                Name = null!;
            }

            public int Id { get; init; }
            public string Name { get; init; }
        }
        #endregion
    }
}
