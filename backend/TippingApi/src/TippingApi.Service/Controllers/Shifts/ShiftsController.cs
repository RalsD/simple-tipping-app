using MediatR;
using Microsoft.AspNetCore.Mvc;
using TippingApi.Application.Shifts;
using TippingApi.Application.Shifts.AddShift;
using TippingApi.Application.Shifts.GetShiftById;
using TippingApi.Application.Shifts.GetShiftsForWeek;
using TippingApi.Domain.Abstractions;

namespace TippingApi.Api.Controllers.Shifts;

[ApiController]
[Route("api/shifts")]
public class ShiftsController : ControllerBase
{
    private readonly ISender _sender;

    public ShiftsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetShiftById(Guid id, CancellationToken ct)
    {
        var query = new GetShiftByIdQuery(id);
        var result = await _sender.Send(query, ct);

        if (result.IsFailure) return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("week")]
    public async Task<IActionResult> GetShiftsForWeek([FromQuery] DateTime weekStart, CancellationToken ct)
    {
        var query = new GetShiftsForWeekQuery(weekStart);

        Result<IReadOnlyList<ShiftResponse>> result = await _sender.Send(query, ct);

        if (result.IsFailure)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddShiftCommand command, CancellationToken ct)
    {
        var result = await _sender.Send(command, ct);

        if (result.IsFailure) return BadRequest(result.Error);

        return CreatedAtAction(nameof(GetShiftById), new { id = result.Value }, null);
    }
}
