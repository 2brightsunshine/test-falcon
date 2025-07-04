using Basket.API.Basket.CheckoutBasket;
using Basket.API.Dtos;
using Carter;
using Carter.Testing;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Net;
using System.Text;
using System.Text.Json;
using Xunit;

namespace Basket.API.Tests.Basket.CheckoutBasket;

public class CheckoutBasketEndpointsTests
{
    private readonly Mock<ISender> _mockSender;
    private readonly CheckoutBasketEndpoints _endpoints;

    public CheckoutBasketEndpointsTests()
    {
        _mockSender = new Mock<ISender>();
        _endpoints = new CheckoutBasketEndpoints();
    }

    [Fact]
    public async Task CheckoutBasket_ValidRequest_ReturnsOkResult()
    {
        // Arrange
        var basketCheckoutDto = CreateValidBasketCheckoutDto();
        var request = new CheckoutBasketRequest(basketCheckoutDto);
        var expectedResult = new CheckoutBasketResult(true);
        
        _mockSender.Setup(x => x.Send(It.IsAny<CheckoutBasketCommand>(), It.IsAny<CancellationToken>()))
                   .ReturnsAsync(expectedResult);

        var app = TestHost.CreateTestHost(_endpoints, services =>
        {
            services.AddSingleton(_mockSender.Object);
        });

        var client = app.GetTestClient();

        // Act
        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/basket/checkout", content);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<CheckoutBasketResponse>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        
        // Verify the command was sent
        _mockSender.Verify(x => x.Send(It.IsAny<CheckoutBasketCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task CheckoutBasket_CommandHandlerThrowsException_ReturnsBadRequest()
    {
        // Arrange
        var basketCheckoutDto = CreateValidBasketCheckoutDto();
        var request = new CheckoutBasketRequest(basketCheckoutDto);
        
        _mockSender.Setup(x => x.Send(It.IsAny<CheckoutBasketCommand>(), It.IsAny<CancellationToken>()))
                   .ThrowsAsync(new InvalidOperationException("Test exception"));

        var app = TestHost.CreateTestHost(_endpoints, services =>
        {
            services.AddSingleton(_mockSender.Object);
        });

        var client = app.GetTestClient();

        // Act
        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/basket/checkout", content);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CheckoutBasket_InvalidJsonRequest_ReturnsBadRequest()
    {
        // Arrange
        var app = TestHost.CreateTestHost(_endpoints, services =>
        {
            services.AddSingleton(_mockSender.Object);
        });

        var client = app.GetTestClient();

        // Act
        var response = await client.PostAsync("/basket/checkout", new StringContent("invalid json", Encoding.UTF8, "application/json"));

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CheckoutBasket_EmptyRequest_ReturnsBadRequest()
    {
        // Arrange
        var app = TestHost.CreateTestHost(_endpoints, services =>
        {
            services.AddSingleton(_mockSender.Object);
        });

        var client = app.GetTestClient();

        // Act
        var response = await client.PostAsync("/basket/checkout", new StringContent("", Encoding.UTF8, "application/json"));

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CheckoutBasket_CommandReturnsFailureResult_ReturnsOkWithFailure()
    {
        // Arrange
        var basketCheckoutDto = CreateValidBasketCheckoutDto();
        var request = new CheckoutBasketRequest(basketCheckoutDto);
        var expectedResult = new CheckoutBasketResult(false);
        
        _mockSender.Setup(x => x.Send(It.IsAny<CheckoutBasketCommand>(), It.IsAny<CancellationToken>()))
                   .ReturnsAsync(expectedResult);

        var app = TestHost.CreateTestHost(_endpoints, services =>
        {
            services.AddSingleton(_mockSender.Object);
        });

        var client = app.GetTestClient();

        // Act
        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/basket/checkout", content);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<CheckoutBasketResponse>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void CheckoutBasketEndpoints_ImplementsICarterModule()
    {
        // Arrange & Act
        var endpoints = new CheckoutBasketEndpoints();

        // Assert
        Assert.IsAssignableFrom<ICarterModule>(endpoints);
    }

    [Fact]
    public async Task CheckoutBasket_NullBasketCheckoutDto_ReturnsBadRequest()
    {
        // Arrange
        var request = new CheckoutBasketRequest(null!);

        var app = TestHost.CreateTestHost(_endpoints, services =>
        {
            services.AddSingleton(_mockSender.Object);
        });

        var client = app.GetTestClient();

        // Act
        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/basket/checkout", content);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CheckoutBasket_VerifyCommandMapping_CorrectCommandSent()
    {
        // Arrange
        var basketCheckoutDto = CreateValidBasketCheckoutDto();
        var request = new CheckoutBasketRequest(basketCheckoutDto);
        var expectedResult = new CheckoutBasketResult(true);
        
        CheckoutBasketCommand? capturedCommand = null;
        _mockSender.Setup(x => x.Send(It.IsAny<CheckoutBasketCommand>(), It.IsAny<CancellationToken>()))
                   .Callback<CheckoutBasketCommand, CancellationToken>((cmd, ct) => capturedCommand = cmd)
                   .ReturnsAsync(expectedResult);

        var app = TestHost.CreateTestHost(_endpoints, services =>
        {
            services.AddSingleton(_mockSender.Object);
        });

        var client = app.GetTestClient();

        // Act
        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/basket/checkout", content);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(capturedCommand);
        Assert.Equal(basketCheckoutDto.UserName, capturedCommand.BasketCheckoutDto.UserName);
        Assert.Equal(basketCheckoutDto.TotalPrice, capturedCommand.BasketCheckoutDto.TotalPrice);
    }

    [Fact]
    public async Task CheckoutBasket_VerifyEndpointRouting_CorrectEndpointCalled()
    {
        // Arrange
        var basketCheckoutDto = CreateValidBasketCheckoutDto();
        var request = new CheckoutBasketRequest(basketCheckoutDto);
        var expectedResult = new CheckoutBasketResult(true);
        
        _mockSender.Setup(x => x.Send(It.IsAny<CheckoutBasketCommand>(), It.IsAny<CancellationToken>()))
                   .ReturnsAsync(expectedResult);

        var app = TestHost.CreateTestHost(_endpoints, services =>
        {
            services.AddSingleton(_mockSender.Object);
        });

        var client = app.GetTestClient();

        // Act
        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/basket/checkout", content);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        // Verify wrong endpoint returns 404
        var wrongEndpointResponse = await client.PostAsync("/basket/wrong-endpoint", content);
        Assert.Equal(HttpStatusCode.NotFound, wrongEndpointResponse.StatusCode);
    }

    private static BasketCheckoutDto CreateValidBasketCheckoutDto()
    {
        return new BasketCheckoutDto
        {
            UserName = "testuser",
            CustomerId = Guid.NewGuid(),
            TotalPrice = 100.50m,
            FirstName = "John",
            LastName = "Doe",
            EmailAddress = "john.doe@test.com",
            AddressLine = "123 Test Street",
            Country = "USA",
            State = "CA",
            ZipCode = "12345",
            CardName = "John Doe",
            CardNumber = "1234567890123456",
            Expiration = "12/25",
            CVV = "123",
            PaymentMethod = 1
        };
    }
}