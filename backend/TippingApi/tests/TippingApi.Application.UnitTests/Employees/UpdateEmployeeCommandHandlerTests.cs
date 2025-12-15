using Moq;
using TippingApi.Application.Employees.UpdateEmployee;
using TippingApi.Domain.Abstractions;
using TippingApi.Domain.Employees;

namespace TippingApi.Application.UnitTests.Employees;

public class UpdateEmployeeCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_Return_Failure_If_Employee_Not_Found()
    {
        var mockRepo = new Mock<IEmployeeRepository>();
        mockRepo.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Employee?)null);

        var handler = new UpdateEmployeeCommandHandler(mockRepo.Object);
        var command = new UpdateEmployeeCommand(Guid.NewGuid(), "NewFirst", "NewLast");

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal(Error.NotFound, result.Error);
    }
}
