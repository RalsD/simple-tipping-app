using MediatR;
using Microsoft.AspNetCore.Mvc;
using TippingApi.Application.Tips.AddTip;
using TippingApi.Application.Tips.CalculateWeeklySplit;
using TippingApi.Application.Tips.GetTipById;

namespace TippingApi.Api.Controllers.Tips;

[ApiController]
[Route("api/tips")]
public class TipsController : ControllerBase
{
    private readonly ISender _sender;

    public TipsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTipById(Guid id, CancellationToken ct)
    {
        var query = new GetTipByIdQuery(id);
        var result = await _sender.Send(query, ct);

        if (result.IsFailure)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("weekly-split")]
    public async Task<IActionResult> GetWeeklySplit([FromQuery] DateTime weekStart, CancellationToken ct)
    {
        var query = new GetWeeklyTipSplitQuery(weekStart);
        var result = await _sender.Send(query, ct);

        if (result.IsFailure) return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> AddTip([FromBody] AddTipCommand command, CancellationToken ct)
    {
        var result = await _sender.Send(command, ct);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return CreatedAtAction(nameof(GetTipById), new { id = result.Value }, null);
    }
}
