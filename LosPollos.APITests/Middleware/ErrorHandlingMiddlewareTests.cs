
using FluentAssertions;
using LosPollos.Domain.Entities;
using LosPollos.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace LosPollos.API.Middleware.Tests
{

    public class ErrorHandlingMiddlewareTests
    {
        [Fact()]
        public async Task InvokeAsync_WhenNoExcepitonOccurs_ShouldCallNextMiddelWare()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();

            var middelWare = new ErrorHandlingMiddleware(loggerMock.Object);
            var httpContext = new DefaultHttpContext();
            var RequestDelegateMock = new Mock<RequestDelegate>();
            //Act
            await middelWare.InvokeAsync(httpContext, RequestDelegateMock.Object);

            //Assert
            RequestDelegateMock.Verify(x => x.Invoke(httpContext), Times.Once);

        }

        [Fact()]
        public async Task InvokeAsync_NotFoundExceptionOccure_ShouldReturnStatusCode404()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();

            var middelWare = new ErrorHandlingMiddleware(loggerMock.Object);
            var httpContext = new DefaultHttpContext();
            var RequestDelegateMock = new Mock<RequestDelegate>();
            //Act
            await middelWare.InvokeAsync(httpContext, _=>throw new NotFoundException(nameof(Resturant),"1"));

            //Assert
            httpContext.Response.StatusCode.Should().Be(404);

        }

        [Fact()]
        public async Task InvokeAsync_ForbidExceptionOccurs_ShouldReturnStatusCode403()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();

            var middelWare = new ErrorHandlingMiddleware(loggerMock.Object);
            var httpContext = new DefaultHttpContext();
            var RequestDelegateMock = new Mock<RequestDelegate>();
            //Act
            await middelWare.InvokeAsync(httpContext, _ => throw new ForbidException());

            //Assert
            httpContext.Response.StatusCode.Should().Be(403);

        }

        [Fact()]
        public async Task InvokeAsync_UserExceptionOccures_ShouldReturnStatusCode400()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();

            var middelWare = new ErrorHandlingMiddleware(loggerMock.Object);
            var httpContext = new DefaultHttpContext();
            var RequestDelegateMock = new Mock<RequestDelegate>();
            //Act
            await middelWare.InvokeAsync(httpContext, _ => throw new UserException());

            //Assert
            httpContext.Response.StatusCode.Should().Be(400);

        }
        [Fact()]
        public async Task InvokeAsync_GeneralException_ShouldReturnStatusCode500()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();

            var middelWare = new ErrorHandlingMiddleware(loggerMock.Object);
            var httpContext = new DefaultHttpContext();
            var RequestDelegateMock = new Mock<RequestDelegate>();
            //Act
            await middelWare.InvokeAsync(httpContext, _ => throw new Exception());

            //Assert
            httpContext.Response.StatusCode.Should().Be(500);

        }

    }
}