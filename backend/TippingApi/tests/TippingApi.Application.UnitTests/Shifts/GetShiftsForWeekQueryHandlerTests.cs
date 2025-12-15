using Moq;
using TippingApi.Application.Shifts.GetShiftsForWeek;
using TippingApi.Domain.Employees;
using TippingApi.Domain.Shifts;

namespace TippingApi.Application.UnitTests.Shifts;

public class GetShiftsForWeekQueryHandlerTests
{
    [Fact]
    public async Task Handle_Should_Return_Shifts_For_Week()
    {
        var weekStart = DateTime.Today;
        var shifts = new List<Shift>
        {
            Shift.Create(Employee.Create(new FirstName("Alice"), new LastName("Smith")),
                         weekStart, TimeSpan.FromHours(9), TimeSpan.FromHours(17))
        };

        var mockRepo = new Mock<IShiftRepository>();
        mockRepo.Setup(x => x.GetShiftsForWeekAsync(weekStart, weekStart.AddDays(7)))
                .ReturnsAsync(shifts);

        var handler = new GetShiftsForWeekQueryHandler(mockRepo.Object);
        var query = new GetShiftsForWeekQuery(weekStart);

        var result = await handler.Handle(query, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal("Alice", result.Value?.First().EmployeeFirstName);
        Assert.Equal("Smith", result.Value?.First().EmployeeLastName);
    }
}
