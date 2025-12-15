using MediatR;
using Microsoft.AspNetCore.Mvc;
using TippingApi.Application.Employees.CreateEmployee;
using TippingApi.Application.Employees.GetEmployee;
using TippingApi.Application.Employees.UpdateEmployee;

namespace TippingApi.Service.Controllers.Employees;

[ApiController]
[Route("api/employees")]
public class EmployeesController : ControllerBase
{
    private readonly ISender _sender;

    public EmployeesController(ISender sender)
    {
        _sender = sender;
    }

    //[HttpGet]
    //public async Task<IActionResult> GetAll(CancellationToken ct)
    //{
    //    var query = new GetAllEmployeesQuery();
    //    var result = await _sender.Send(query, ct);
    //    return Ok(result.Value);
    //}

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var query = new GetEmployeeByIdQuery(id);
        var result = await _sender.Send(query, ct);

        if (result.IsFailure) return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeCommand command, CancellationToken ct)
    {
        var result = await _sender.Send(command, ct);

        if (result.IsFailure) return BadRequest(result.Error);

        return CreatedAtAction(nameof(GetById), new { id = result.Value }, null);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEmployeeCommand command, CancellationToken ct)
    {
        var commandWithId = command with { EmployeeId = id };
        var result = await _sender.Send(commandWithId, ct);

        if (result.IsFailure) return NotFound(result.Error);

        return NoContent();
    }
}
