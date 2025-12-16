namespace TippingApi.Application.DTOs;

public class ShiftDto
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public double HoursWorked { get; set; }
}

