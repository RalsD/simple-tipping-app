using TippingApi.Application.Abstractions.Messaging;
using TippingApi.Application.Shifts.GetShiftById;
using TippingApi.Domain.Abstractions;
using TippingApi.Domain.Shifts;

namespace TippingApi.Application.Shifts.Queries;

public sealed class GetShiftByIdQueryHandler
    : IQueryHandler<GetShiftByIdQuery, ShiftResponse>
{
    private readonly IShiftRepository _shiftRepository;

    public GetShiftByIdQueryHandler(IShiftRepository shiftRepository)
    {
        _shiftRepository = shiftRepository;
    }

    public async Task<Result<ShiftResponse>> Handle(
        GetShiftByIdQuery request,
        CancellationToken cancellationToken)
    {
        var shift = await _shiftRepository.GetByIdAsync(request.ShiftId);

        if (shift is null)
            return Result.Failure<ShiftResponse>(Error.NotFound);

        var response = new ShiftResponse(
            shift.Id,
            shift.Employee.Id,
            shift.Employee.FirstName.Value,
            shift.Employee.LastName.Value,
            shift.Date,
            shift.StartTime,
            shift.EndTime,
            shift.HoursWorked()
        );

        return Result.Success(response);
    }
}
