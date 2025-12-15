using Moq;
using TippingApi.Application.Employees.CreateEmployee;
using TippingApi.Domain.Employees;

namespace TippingApi.Application.UnitTests.Employees;

public class CreateEmployeeCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_Add_Employee_And_Return_Success()
    {
        // Arrange
        var mockRepo = new Mock<IEmployeeRepository>();
        var handler = new CreateEmployeeCommandHandler(mockRepo.Object);

        var command = new CreateEmployeeCommand("Alice", "Smith");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        mockRepo.Verify(x => x.AddAsync(It.IsAny<Employee>()), Times.Once);
    }
}
