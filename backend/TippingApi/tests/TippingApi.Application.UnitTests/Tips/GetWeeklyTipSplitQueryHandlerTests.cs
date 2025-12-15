using Moq;
using TippingApi.Application.Tips.CalculateWeeklySplit;
using TippingApi.Domain.Employees;
using TippingApi.Domain.Shifts;
using TippingApi.Domain.Tips;

namespace TippingApi.Application.UnitTests.Tips;

public class GetWeeklyTipSplitQueryHandlerTests
{
    [Fact]
    public async Task Handle_Should_Calculate_Correct_Split()
    {
        var weekStart = DateTime.Today;

        var phil = Employee.Create(new FirstName("Philip"), new LastName("Smith"));
        var jason = Employee.Create(new FirstName("Jason"), new LastName("Jones"));

        var shifts = new List<Shift>
        {
            Shift.Create(phil, weekStart, TimeSpan.FromHours(9), TimeSpan.FromHours(17)), // 8 hours
            Shift.Create(jason, weekStart, TimeSpan.FromHours(9), TimeSpan.FromHours(13)) // 4 hours
        };

        var mockShiftRepo = new Mock<IShiftRepository>();
        mockShiftRepo.Setup(x => x.GetShiftsForWeekAsync(weekStart, weekStart.AddDays(7)))
                     .ReturnsAsync(shifts);

        var mockTipRepo = new Mock<ITipRepository>();
        mockTipRepo.Setup(x => x.GetWeeklyTotalAsync(weekStart, weekStart.AddDays(7)))
                   .ReturnsAsync(120m); // total tips

        var handler = new GetWeeklyTipSplitQueryHandler(mockTipRepo.Object, mockShiftRepo.Object);
        var query = new GetWeeklyTipSplitQuery(weekStart);

        var result = await handler.Handle(query, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Value?.Count);
        Assert.Equal(80m, result.Value[phil.Id]);
        Assert.Equal(40m, result.Value[jason.Id]);
    }
}
