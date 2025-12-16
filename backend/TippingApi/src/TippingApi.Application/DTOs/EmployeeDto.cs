namespace TippingApi.Application.DTOs;

public class EmployeeDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public List<ShiftDto> Shifts { get; set; } = new();
}

